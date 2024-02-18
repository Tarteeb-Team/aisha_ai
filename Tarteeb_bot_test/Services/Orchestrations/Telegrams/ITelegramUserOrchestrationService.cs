using System.Threading.Tasks;
using System;
using Tarteeb_bot_test.Models.TelegramUserMessages;

namespace Tarteeb_bot_test.Services.Orchestrations.Telegrams
{
    public interface ITelegramUserOrchestrationService
    {
        void ListenAndRegisterTelegramUserMessage(Func<TelegramUserMessage, ValueTask> eventHandler);
    }
}
