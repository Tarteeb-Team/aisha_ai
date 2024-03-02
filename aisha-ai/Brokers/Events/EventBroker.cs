using LeVent.Clients;
using Tarteeb_bot_test.Models.TelegramUserMessages;

namespace Tarteeb_bot_test.Brokers.Events
{
    public partial class EventBroker : IEventBroker
    {
        public EventBroker()
        {
            this.TelegramUserMessageEvents = new LeVentClient<TelegramUserMessage>();
        }
    }
}
