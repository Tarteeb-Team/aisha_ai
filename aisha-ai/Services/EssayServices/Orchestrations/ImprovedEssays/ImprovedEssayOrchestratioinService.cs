using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.EssayEvents;
using aisha_ai.Models.EssayModels.ImprovedEssays;
using aisha_ai.Services.EssayServices.Foundations.ImprovedEssays;
using aisha_ai.Services.EssayServices.Orchestrations.ImprovedEssays;
using aisha_ai.Services.Foundations.EssayEvents;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;

namespace aisha_ai.Services.Orchestrations.ImprovedEssays
{
    public class ImprovedEssayOrchestratioinService : IImprovedEssayOrchestratioinService
    {
        private readonly IEssayEventService essayEventService;
        private readonly IImproveEssayService improveEssayService;
        private readonly IImprovedEssayService improvedEssayService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;

        public ImprovedEssayOrchestratioinService(
            IEssayEventService essayEventService,
            IImproveEssayService improveEssayService,
            IImprovedEssayService improvedEssayService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService)
        {
            this.essayEventService = essayEventService;
            this.improveEssayService = improveEssayService;
            this.improvedEssayService = improvedEssayService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
        }

        public void ListenEssayEvent() =>
            this.essayEventService.ListenToEssayEvent(ProcessEssayEventAsync);

        private async ValueTask ProcessEssayEventAsync(EssayEvent essayEvent)
        {
            var content = await this.improveEssayService.ImproveEssayAsync(essayEvent.Essay.Content);

            var maybeImprovedEssay = this.improvedEssayService.RetrieveAllImprovedEssays()
                .FirstOrDefault(i => i.TelegramUserName == essayEvent.TelegramUser.TelegramUserName);

            if (maybeImprovedEssay is not null)
            {
                await ModifyImprovedEssay(maybeImprovedEssay, content);
            }
            else
            {
                var createdImprovedEssay = PopulateImprovedEssay(content, essayEvent);

                await this.improvedEssayService
                    .AddImprovedEssayAsync(createdImprovedEssay);
            }

            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramUserName == essayEvent.TelegramUser.TelegramUserName);

            await this.telegramService.SendMessageAsync(
                1924521160, $"ImprovedEssay is done\nUser: {telegramUser.TelegramUserName}");
        }


        private ImprovedEssay PopulateImprovedEssay(string content, EssayEvent essayEvent)
        {
            return new ImprovedEssay
            {
                Id = Guid.NewGuid(),
                Content = content,
                TelegramUserName = essayEvent.TelegramUser.TelegramUserName,
                TelegramUserId = essayEvent.TelegramUser.Id
            };
        }

        private async ValueTask ModifyImprovedEssay(ImprovedEssay improvedEssay, string content)
        {
            improvedEssay.Content = content;

            await this.improvedEssayService.ModifyEssayAsync(improvedEssay);
        }
    }
}
