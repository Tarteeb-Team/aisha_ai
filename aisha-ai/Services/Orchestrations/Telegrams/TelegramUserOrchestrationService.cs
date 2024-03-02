using System;
using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Tarteeb_bot_test.Services.Foundations.Telegrams;

namespace Tarteeb_bot_test.Services.Orchestrations.Telegrams
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
