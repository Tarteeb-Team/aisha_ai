using System.Threading.Tasks;
using System;
using Tarteeb_bot_test.Models.ExternalSpeechs;
using Tarteeb_bot_test.Models.ExternalVoices;

namespace Tarteeb_bot_test.Services.Foundations.Levents.ExternalSpeechs
{
    public interface IExternalVoiceEventService
    {
        ValueTask PublishExternalVoiceAsync(
            ExternalVoice externalVoice,
            string eventName = null);

        void ListenToExternalVoiceEvent(
             Func<ExternalVoice, ValueTask> externalVoiceEventHandler,
             string eventName = null);
    }
}
