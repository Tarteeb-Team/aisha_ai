using System;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;
using aisha_ai.Services.Foundations.Telegrams;

namespace aisha_ai.Services.Orchestrations.Telegrams
{
    public class TelegramUserOrchestrationService : ITelegramUserOrchestrationService
    {
        private readonly ITelegramService telegramService;

        public TelegramUserOrchestrationService(ITelegramService telegramService)
        {
            this.telegramService = telegramService;
        }

        public void ListenAndRegisterTelegramUserMessage(Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            this.telegramService.RegisterTelegramEventHandler(async (telegramUserMessage) =>
            {
                await this.ProcessTelegramUserAsync(telegramUserMessage, eventHandler);
            });
        }

        private async ValueTask ProcessTelegramUserAsync(
            TelegramUserMessage telegramUserMessage, 
            Func<TelegramUserMessage, ValueTask> eventHandler)
        {

            await eventHandler(telegramUserMessage);
        }
    }
}
