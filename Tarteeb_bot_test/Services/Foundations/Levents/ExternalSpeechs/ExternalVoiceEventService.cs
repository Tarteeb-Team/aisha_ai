using System;
using System.Threading.Tasks;
using Tarteeb_bot_test.Brokers.Events;
using Tarteeb_bot_test.Models.ExternalSpeechs;
using Tarteeb_bot_test.Models.ExternalVoices;

namespace Tarteeb_bot_test.Services.Foundations.Levents.ExternalSpeechs
{
    public class ExternalVoiceEventService : IExternalVoiceEventService
    {
        private readonly IEventBroker eventBroker;

        public ExternalVoiceEventService(IEventBroker eventBroker) =>
            this.eventBroker = eventBroker;

        public ValueTask PublishExternalVoiceAsync(
            ExternalVoice externalVoice,
            string eventName = null)
        {
            return this.eventBroker.PublishExternalVoiceAsync(
                externalVoice: externalVoice,
                eventName: eventName);
        }

        public void ListenToExternalVoiceEvent(
            Func<ExternalVoice, ValueTask> externalVoiceEventHandler,
            string eventName = null)
        {
            this.eventBroker.ListenToExternalVoiceEvent(
                externalVoiceEventHandler: externalVoiceEventHandler,
                eventName: eventName);
        }
    }
}
