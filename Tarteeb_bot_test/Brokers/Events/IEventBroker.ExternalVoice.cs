using System.Threading.Tasks;
using System;
using Tarteeb_bot_test.Models.ExternalVoices;

namespace Tarteeb_bot_test.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishExternalVoiceAsync(
          ExternalVoice externalVoice,
          string eventName = null);

        void ListenToExternalVoiceEvent(
            Func<ExternalVoice, ValueTask> externalVoiceEventHandler,
            string eventName = null);
    }
}
