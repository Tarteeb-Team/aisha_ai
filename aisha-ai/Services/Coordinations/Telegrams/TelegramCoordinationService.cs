using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Tarteeb_bot_test.Services.Orchestrations.Telegrams;
using Tarteeb_bot_test.Services.Orchestrations.TelegramStates;

namespace Tarteeb_bot_test.Services.Coordinations.Telegrams
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
