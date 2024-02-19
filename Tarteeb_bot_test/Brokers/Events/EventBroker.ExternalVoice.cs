using LeVent.Clients;
using System.Threading.Tasks;
using System;
using Tarteeb_bot_test.Models.ExternalVoices;

namespace Tarteeb_bot_test.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<ExternalVoice> ExternalVoiceEvents { get; set; }
        public ValueTask PublishExternalVoiceAsync(
            ExternalVoice externalVoice,
            string eventName = null)
        {
            return this.ExternalVoiceEvents.PublishEventAsync(externalVoice, eventName);
        }

        public void ListenToExternalVoiceEvent(
            Func<ExternalVoice, ValueTask> externalVoiceEventHandler,
            string eventName = null)
        {
            this.ExternalVoiceEvents.RegisterEventHandler(externalVoiceEventHandler, eventName);
        }
    }
}
