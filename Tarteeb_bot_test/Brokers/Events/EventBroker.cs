using System.Runtime.CompilerServices;
using LeVent.Clients;
using Tarteeb_bot_test.Models.ExternalVoices;
using Tarteeb_bot_test.Models.TelegramUserMessages;

namespace Tarteeb_bot_test.Brokers.Events
{
    public partial class EventBroker : IEventBroker
    {
        public EventBroker()
        {
            this.TelegramUserMessageEvents = new LeVentClient<TelegramUserMessage>();
            this.ExternalVoiceEvents = new LeVentClient<ExternalVoice>();
        }
    }
}
