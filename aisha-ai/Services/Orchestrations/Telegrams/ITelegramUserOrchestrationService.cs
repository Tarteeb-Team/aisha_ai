using System;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;

namespace aisha_ai.Services.Orchestrations.Telegrams
{
    public interface ITelegramUserOrchestrationService
    {
        void ListenAndRegisterTelegramUserMessage(Func<TelegramUserMessage, ValueTask> eventHandler);
    }
}
