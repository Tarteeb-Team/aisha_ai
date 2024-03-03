using System;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;
using LeVent.Clients;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<TelegramUserMessage> TelegramUserMessageEvents { get; set; }
        public ValueTask PublishTelegramUserMessageAsync(
            TelegramUserMessage telegramUserMessage, 
            string eventName = null)
        {
            return this.TelegramUserMessageEvents.PublishEventAsync(telegramUserMessage, eventName);
        }

        public void ListenToTelegramUserMessageEvent(
            Func<TelegramUserMessage, ValueTask> telegramUserMessageEventHandler, 
            string eventName = null)
        {
            this.TelegramUserMessageEvents.RegisterEventHandler(telegramUserMessageEventHandler, eventName);
        }
    }
}
