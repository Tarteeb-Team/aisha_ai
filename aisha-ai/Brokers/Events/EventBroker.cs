using aisha_ai.Models.TelegramUserMessages;
using LeVent.Clients;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker : IEventBroker
    {
        public EventBroker()
        {
            this.TelegramUserMessageEvents = new LeVentClient<TelegramUserMessage>();
        }
    }
}
