using System;
using System.Linq;
using System.Threading.Tasks;
using Aisha.Core.Services.Orchestrations.Essays;
using aisha_ai.Models.EssayModels.EssayEvents;
using aisha_ai.Models.EssayModels.Essays;
using aisha_ai.Models.EssayModels.ImageMetadatas;
using aisha_ai.Services.EssayServices.Foundations.Events.EssayEvents;
using aisha_ai.Services.EssayServices.Foundations.Events.ImageMetadataEvents;
using aisha_ai.Services.EssayServices.Orchestrations.ImprovedEssays;
using aisha_ai.Services.Foundations.Essays;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.Foundations.Visions;

namespace aisha_ai.Services.EssayServices.Orchestrations.Essays
{
    public class EssayOrchestrationService : IEssayOrchestrationService
    {
        private readonly IImageMeatadataEventService imageMetadataEventService;
        private readonly IVisionService visionService;
        private readonly IEssayEventService essayEventService;
        private readonly IImprovedEssayOrchestratioinService improvedEssayOrchestratioinService;
        private readonly IEssayService essayService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;

        public EssayOrchestrationService(
            IImageMeatadataEventService imageMetadataEventService,
            IVisionService visionService,
            IEssayEventService essayEventService,
            IImprovedEssayOrchestratioinService improvedEssayOrchestratioinService,
            IEssayService essayService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService)
        {
            this.imageMetadataEventService = imageMetadataEventService;
            this.visionService = visionService;
            this.essayEventService = essayEventService;
            this.improvedEssayOrchestratioinService = improvedEssayOrchestratioinService;
            this.essayService = essayService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
        }

        public void ListenImageMetadata(Func<Essay, ValueTask> essayAnalyseHandler)
        {
            imageMetadataEventService.ListenToImageMetadataEvent(async (imageMetadata) =>
            {
                await ProcessImageMetadataAsync(imageMetadata, essayAnalyseHandler);
            });
        }

        private async ValueTask ProcessImageMetadataAsync(
            ImageMetadata imageMetadata,
            Func<Essay, ValueTask> essayAnalyseHandler)
        {
            Essay actualEssay = await EnsureEssayAsync(imageMetadata);

            var telegramUser = telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramUserName == imageMetadata.TelegramUser.TelegramUserName);

            await telegramService.SendMessageAsync(
                1924521160, $"Photo to text is done\nUser: {telegramUser.TelegramUserName}");

            EssayEvent essayEvent = PopulateEssayEvent(actualEssay, imageMetadata);
            essayEventService.PublishEssayEventAsync(essayEvent); // to improve essay
            await essayAnalyseHandler(actualEssay);
        }

        private async Task<Essay> EnsureEssayAsync(ImageMetadata imageMetadata)
        {
            Essay actualEssay = null;

            var maybeEssay = essayService.RetrieveAllEssays()
                .FirstOrDefault(m => m.TelegramUserName == imageMetadata.TelegramUser.TelegramUserName);

            if (maybeEssay is not null)
            {
                await ModifyEssay(imageMetadata, maybeEssay);
                actualEssay = maybeEssay;
            }
            else
            {
                Essay essay = await PopulateEssay(imageMetadata);
                await essayService.AddEssayAsync(essay);
                actualEssay = essay;
            }

            return actualEssay;
        }

        private EssayEvent PopulateEssayEvent(Essay essay, ImageMetadata imageMetadata)
        {
            return new EssayEvent
            {
                Essay = essay,
                TelegramUser = imageMetadata.TelegramUser
            };
        }

        private async Task<Essay> PopulateEssay(ImageMetadata imageMetadata)
        {
            imageMetadata.ImageStream.Position = 0;
            var text = await visionService.ExtractTextAsync(imageMetadata.ImageStream);

            return new Essay
            {
                Id = Guid.NewGuid(),
                Content = text,
                TelegramUserName = imageMetadata.TelegramUser.TelegramUserName,
                TelegramUserId = imageMetadata.TelegramUser.Id,
            };
        }

        private async Task<string> ModifyEssay(ImageMetadata imageMetadata, Essay essay)
        {
            try
            {
                imageMetadata.ImageStream.Position = 0;
                var text = await visionService.ExtractTextAsync(imageMetadata.ImageStream);
                essay.Content = text;
                await essayService.ModifyEssayAsync(essay);

                return text;
            }
            catch (Exception ex)
            {
                await telegramService.SendMessageAsync(1924521160, $"{ex.Message}\nUser: {essay.TelegramUserName}");

                throw;
            }
        }
    }
}
