using System;
using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUserMessages;

namespace Tarteeb_bot_test.Brokers.Events
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
