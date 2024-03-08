using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.TelegramUserMessages;

namespace aisha_ai.Services.EssayServices.Orchestrations.Telegrams
{
    public interface ITelegramUserOrchestrationService
    {
        void ListenAndRegisterTelegramUserMessage(Func<TelegramUserMessage, ValueTask> eventHandler);
    }
}
