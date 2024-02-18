using System;
using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Tarteeb_bot_test.Services.Foundations.Telegrams;
using Tarteeb_bot_test.Services.Processings.TelegramUsers;
using Tarteeb_bot_test.Services.Processings.Users;

namespace Tarteeb_bot_test.Services.Orchestrations.Telegrams
{
    public class TelegramUserOrchestrationService : ITelegramUserOrchestrationService
    {
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserProcessingSerivce telegramUserProcessingSerivce;
        private readonly IUserProcessingService userProcessingService;

        public TelegramUserOrchestrationService(
            ITelegramService telegramService,
            ITelegramUserProcessingSerivce telegramUserProcessingSerivce,
            IUserProcessingService userProcessingService)
        {
            this.telegramService = telegramService;
            this.telegramUserProcessingSerivce = telegramUserProcessingSerivce;
            this.userProcessingService = userProcessingService;
        }

        public void ListenAndRegisterTelegramUserMessage(Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            this.telegramService.RegisterTelegramEventHandler(async (telegramUserMessage) =>
            {
                await this.ProcessTelegramUserAsync(telegramUserMessage, eventHandler);
            });
        }

        private async ValueTask ProcessTelegramUserAsync(TelegramUserMessage telegramUserMessage, Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            var ensureUser = await this.userProcessingService.ReturnTrue();
            var ensureTelegramUser = await this.telegramUserProcessingSerivce.ReturnTrue();

            if (ensureUser is true
                && ensureTelegramUser is true)
            {
                await eventHandler(telegramUserMessage);

            }
        }
    }
}
