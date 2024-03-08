using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.TelegramUserMessages;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Processings.TelegramUsers;

namespace aisha_ai.Services.EssayServices.Orchestrations.Telegrams
{
    public class TelegramUserOrchestrationService : ITelegramUserOrchestrationService
    {
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserProcessingService telegramUserProcessingService;

        public TelegramUserOrchestrationService(
            ITelegramService telegramService,
            ITelegramUserProcessingService telegramUserProcessingService)
        {
            this.telegramService = telegramService;
            this.telegramUserProcessingService = telegramUserProcessingService;
        }

        public void ListenAndRegisterTelegramUserMessage(Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            telegramService.RegisterTelegramEventHandler(async (telegramUserMessage) =>
            {
                await ProcessTelegramUserAsync(telegramUserMessage, eventHandler);
            });
        }

        private async ValueTask ProcessTelegramUserAsync(
            TelegramUserMessage telegramUserMessage,
            Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            await telegramUserProcessingService.EnsureTelegramUserAsync(telegramUserMessage.TelegramUser);

            await eventHandler(telegramUserMessage);
        }
    }
}
