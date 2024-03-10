using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using aisha_ai.Models.EssayModels.ImageMetadatas;
using aisha_ai.Models.EssayModels.TelegramUserMessages;
using aisha_ai.Models.EssayModels.TelegramUsers;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;
using aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers;
using aisha_ai.Services.Foundations.ImageMetadataEvents;
using aisha_ai.Services.Foundations.PhotoCheckers;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Services.Orchestrations.TelegramStates
{
    public class TelegramStateOrchestrationService : ITelegramStateOrchestrationService
    {
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;
        private readonly IPhotoCheckersService checkerService;
        private readonly IImageMeatadataEventService imageMeatadataEventService;
        private readonly IFeedbackCheckerService feedbackCheckerService;

        public TelegramStateOrchestrationService(
            ITelegramService telegramService,
            ITelegramUserService telegramUserService,
            IImageMeatadataEventService imageMeatadataEventService,
            IPhotoCheckersService checkerService,
            IFeedbackCheckerService feedbackCheckerService)
        {
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
            this.imageMeatadataEventService = imageMeatadataEventService;
            this.checkerService = checkerService;
            this.feedbackCheckerService = feedbackCheckerService;
        }

        public async ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage)
        {
            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramId == telegramUserMessage.TelegramUser.TelegramId);

            if (telegramUserMessage.Message.Text is "/start")
            {
                telegramUser.TelegramUserStatus = TelegramUserStatus.Active;
                await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

                await PopulateCheckerAndAddAsync(telegramUserMessage.TelegramUser);
                await PopulateFeedbackCheckerAndAddAsync(telegramUserMessage.TelegramUser);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    replyMarkup: new ReplyKeyboardMarkup("Upload essay 📝") { ResizeKeyboard = true },
                    message: "Welcome, it's me Aisha 🤵🏻‍♀️");

                return;
            }
            if (telegramUserMessage.Message.Text is "Upload essay 📝"
                && telegramUser?.TelegramUserStatus is TelegramUserStatus.Active)
            {
                telegramUser.TelegramUserStatus = TelegramUserStatus.UploadEssay;
                await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "You can do it, just send me a high-quality photo of your essay 📝");

                return;
            }
            if (telegramUserMessage.Message.Type is MessageType.Photo
                && telegramUser?.TelegramUserStatus is TelegramUserStatus.UploadEssay)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    replyMarkup: new ReplyKeyboardRemove(),
                    message: "Nice 😄");

                ImageMetadata imageMetadata = await PopulateImageMetadataAsync(
                    telegramUserMessage.Message,
                    telegramUserMessage.TelegramUser);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                await ModifyCheckerAsync(telegramUserMessage.TelegramUser);
                await this.imageMeatadataEventService.PublishImageMetadataEventAsync(imageMetadata);
                stopwatch.Stop();

                await this.telegramService.SendMessageAsync(
                   userTelegramId: 1924521160,
                   replyMarkup: new ReplyKeyboardMarkup("/start") { ResizeKeyboard = true },
                   message: $"Time: {stopwatch.Elapsed}\nUser: {telegramUser.TelegramUserName}");
            }
        }

        private async ValueTask<ImageMetadata> PopulateImageMetadataAsync(
            Message message,
            TelegramUser telegramUser)
        {
            var imageFile = await telegramService
                .GetFileAsync(message.Photo[message.Photo.Length - 1].FileId);

            var imageStream = new MemoryStream();
            await this.telegramService.DownloadFileAsync(imageFile.FilePath, imageStream);

            return new ImageMetadata
            {
                ImageStream = imageStream,
                TelegramUser = telegramUser
            };
        }

        private async ValueTask PopulateCheckerAndAddAsync(TelegramUser telegramUser)
        {
            var maybeChecker = this.checkerService.RetrieveAllPhotoCheckers()
                .FirstOrDefault(c => c.TelegramUserName == telegramUser.TelegramUserName);

            if (maybeChecker is not null)
            {
                return;
            }
            else
            {
                var checker = new PhotoChecker
                {
                    Id = Guid.NewGuid(),
                    State = false,
                    TelegramUserId = telegramUser.Id,
                    TelegramUserName = telegramUser.TelegramUserName
                };

                await this.checkerService.AddPhotoCheckerAsync(checker);
                telegramUser.CheckerId = checker.Id;
                await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);
            }
        }

        private async ValueTask PopulateFeedbackCheckerAndAddAsync(TelegramUser telegramUser)
        {
            var maybeFeedbackChecker = this.feedbackCheckerService.RetrieveAllFeedbackCheckers()
                .FirstOrDefault(c => c.TelegramUserName == telegramUser.TelegramUserName);

            if (maybeFeedbackChecker is not null)
                return;
            else
            {
                var feedbackChecker = new FeedbackChecker
                {
                    Id = Guid.NewGuid(),
                    State = false,
                    TelegramUserId = telegramUser.Id,
                    TelegramUserName = telegramUser.TelegramUserName
                };

                await this.feedbackCheckerService.AddFeedbackCheckerAsync(feedbackChecker);
                telegramUser.FeedbackCheckerId = feedbackChecker.Id;
                await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);
            }
        }

        private async ValueTask ModifyCheckerAsync(TelegramUser telegramUser)
        {
            var checker = this.checkerService.RetrieveAllPhotoCheckers()
                .FirstOrDefault(c => c.TelegramUserName == telegramUser.TelegramUserName);

            checker.State = true;
            await this.checkerService.ModifyPhotoCheckerAsync(checker);
        }
    }
}
