using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayEvents;
using aisha_ai.Models.Essays;
using aisha_ai.Models.ImageMetadatas;
using aisha_ai.Services.Foundations.EssayEvents;
using aisha_ai.Services.Foundations.Essays;
using aisha_ai.Services.Foundations.ImageMetadataEvents;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.Foundations.Visions;
using aisha_ai.Services.Orchestrations.ImprovedEssays;

namespace Aisha.Core.Services.Orchestrations.Essays
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
            this.imageMetadataEventService.ListenToImageMetadataEvent(async (imageMetadata) =>
            {
                await ProcessImageMetadataAsync(imageMetadata, essayAnalyseHandler);
            });
        }

        private async ValueTask ProcessImageMetadataAsync(
            ImageMetadata imageMetadata, 
            Func<Essay, ValueTask> essayAnalyseHandler)
        {
            Essay actualEssay = await EnsureEssayAsync(imageMetadata);

            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramUserName == imageMetadata.TelegramUser.TelegramUserName);

            await this.telegramService.SendMessageAsync(telegramUser.TelegramId, "Photo to text is done");

            EssayEvent essayEvent = PopulateEssayEvent(actualEssay, imageMetadata); 
            this.essayEventService.PublishEssayEventAsync(essayEvent); // to improve essay
            await essayAnalyseHandler(actualEssay);
        }

        private async Task<Essay> EnsureEssayAsync(ImageMetadata imageMetadata)
        {
            Essay actualEssay = null;

            var maybeEssay = this.essayService.RetrieveAllEssays()
                .FirstOrDefault(m => m.TelegramUserName == imageMetadata.TelegramUser.TelegramUserName);

            if (maybeEssay is not null)
            {
                await ModifyEssay(imageMetadata, maybeEssay);
                actualEssay = maybeEssay;
            }
            else
            {
                Essay essay = await PopulateEssay(imageMetadata);
                await this.essayService.AddEssayAsync(essay);
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
            var text = await this.visionService.ExtractTextAsync(imageMetadata.ImageStream);

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
                var text = await this.visionService.ExtractTextAsync(imageMetadata.ImageStream);
                essay.Content = text;
                await this.essayService.ModifyEssayAsync(essay);

                return text;
            }
            catch (Exception ex)
            {
                await this.telegramService.SendMessageAsync(1924521160, $"{ex.Message}");
                throw;
            }
        }
    }
}
