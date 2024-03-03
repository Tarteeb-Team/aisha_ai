using System;
using System.Threading.Tasks;
using aisha_ai.Brokers.Events;
using aisha_ai.Models.TelegramUserMessages;

namespace aisha_ai.Services.Foundations.Levents.TelegramEvents
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
