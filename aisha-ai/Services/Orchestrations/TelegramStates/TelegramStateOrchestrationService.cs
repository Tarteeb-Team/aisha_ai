using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Chekers;
using aisha_ai.Models.ImageMetadatas;
using aisha_ai.Models.TelegramUserMessages;
using aisha_ai.Models.TelegramUsers;
using aisha_ai.Services.Foundations.Checkers;
using aisha_ai.Services.Foundations.ImageMetadataEvents;
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
        private readonly ICheckerService checkerService;
        private readonly IImageMeatadataEventService imageMeatadataEventService;

        public TelegramStateOrchestrationService(
            ITelegramService telegramService,
            ITelegramUserService telegramUserService,
            IImageMeatadataEventService imageMeatadataEventService,
            ICheckerService checkerService)
        {
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
            this.imageMeatadataEventService = imageMeatadataEventService;
            this.checkerService = checkerService;
        }

        public async ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage)
        {
            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramId == telegramUserMessage.TelegramUser.TelegramId);

            if (telegramUserMessage.Message.Text is "/start")
            {
                if (telegramUser != null)
                {
                    telegramUser.TelegramUserStatus = TelegramUserStatus.Active;
                    await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

                    await PopulateCheckerAndAddAsync(telegramUserMessage.TelegramUser);

                    await this.telegramService.SendMessageAsync(
                        userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                        replyMarkup: new ReplyKeyboardMarkup("Photo") { ResizeKeyboard = true },
                        message: "HI, you are already registered.");

                    return;
                }
            }
            if (telegramUserMessage.Message.Text is "Photo"
                && telegramUser?.TelegramUserStatus is TelegramUserStatus.Active)
            {
                telegramUser.TelegramUserStatus = TelegramUserStatus.Photo;
                await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Please, send essay.");

                return;
            }
            if (telegramUserMessage.Message.Type is MessageType.Photo
                && telegramUser?.TelegramUserStatus is TelegramUserStatus.Photo)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Accepted, nice.");

                ImageMetadata imageMetadata = await PopulateImageMetadataAsync(
                    telegramUserMessage.Message,
                    telegramUserMessage.TelegramUser);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                await ModifyCheckerAsync(telegramUserMessage.TelegramUser);
                await this.imageMeatadataEventService.PublishImageMetadataEventAsync(imageMetadata);

                stopwatch.Stop();

                await this.telegramService.SendMessageAsync(
                   userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                   message: $"Time: {stopwatch.Elapsed}");
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
            var maybeChecker = this.checkerService.RetrieveAllCheckers()
                .FirstOrDefault(c => c.TelegramUserName == telegramUser.TelegramUserName);

            if (maybeChecker is not null)
            {
                return;
            }
            else
            {
                var checker = new Checker
                {
                    Id = Guid.NewGuid(),
                    State = false,
                    TelegramUserName = telegramUser.TelegramUserName
                };

                await this.checkerService.AddCheckerAsync(checker);
            }
        }

        private async ValueTask ModifyCheckerAsync(TelegramUser telegramUser)
        {
            var checker = this.checkerService.RetrieveAllCheckers()
                .FirstOrDefault(c => c.TelegramUserName == telegramUser.TelegramUserName);

            checker.State = true;
            await this.checkerService.ModifyCheckerAsync(checker);
        }
    }
}
