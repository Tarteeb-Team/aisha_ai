using System;
using System.Threading.Tasks;
using Tarteeb_bot_test.Brokers.Events;
using Tarteeb_bot_test.Models.TelegramUserMessages;

namespace Tarteeb_bot_test.Services.Foundations.Levents.TelegramEvents
{
    public class TelegramUserMessageEventService : ITelegramUserMessageEventService
    {
        private readonly IEventBroker eventBroker;

        public TelegramUserMessageEventService(IEventBroker eventBroker) =>
            this.eventBroker = eventBroker;

        public ValueTask PublishTelegramUserMessageAsync(
            TelegramUserMessage telegramUserMessage,
            string eventName = null)
        {
            return this.eventBroker.PublishTelegramUserMessageAsync(
                telegramUserMessage: telegramUserMessage,
                eventName: eventName);
        }

        public void ListenToTelegramUserMessageEvent(
            Func<TelegramUserMessage, ValueTask> telegramUserMessageEventHandler,
            string eventName = null)
        {
            this.eventBroker.ListenToTelegramUserMessageEvent(
                telegramUserMessageEventHandler: telegramUserMessageEventHandler,
                eventName: eventName);
        }
    }
}
