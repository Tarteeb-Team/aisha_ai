using System;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishTelegramUserMessageAsync(
            TelegramUserMessage telegramUserMessage,
            string eventName = null);

        void ListenToTelegramUserMessageEvent(
            Func<TelegramUserMessage, ValueTask> telegramUserMessageEventHandler,
            string eventName = null);
    }
}
