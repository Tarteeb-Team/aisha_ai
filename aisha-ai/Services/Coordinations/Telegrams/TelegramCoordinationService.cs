using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;
using aisha_ai.Services.Orchestrations.Telegrams;
using aisha_ai.Services.Orchestrations.TelegramStates;

namespace aisha_ai.Services.Coordinations.Telegrams
{
    public class TelegramCoordinationService : ITelegramCoordinationService
    {
        private readonly ITelegramUserOrchestrationService telegramUserOrchestrationService;
        private readonly ITelegramStateOrchestrationService telegramStateOrchestrationService;

        public TelegramCoordinationService(
            ITelegramUserOrchestrationService telegramUserOrchestrationService,
            ITelegramStateOrchestrationService telegramStateOrchestrationService)
        {
            this.telegramUserOrchestrationService = telegramUserOrchestrationService;
            this.telegramStateOrchestrationService = telegramStateOrchestrationService;
        }

        public void ListenTelegramUserMessage()
        {
            this.telegramUserOrchestrationService.ListenAndRegisterTelegramUserMessage(async (telegramUserMessage) =>
            {
                await this.ProcessTelegramUserMessageAsync(telegramUserMessage);
            });
        }

        private async ValueTask ProcessTelegramUserMessageAsync(TelegramUserMessage telegramUserMessage) =>
            await this.telegramStateOrchestrationService.DispatchProcessAsync(telegramUserMessage);
    }
}
