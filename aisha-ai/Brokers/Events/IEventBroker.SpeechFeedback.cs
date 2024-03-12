using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechFeedback;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishSpeechFeedbackAsync(SpeechFeedback speechFeedback, string eventName = null);
        void ListenToSpeechFeedback(Func<SpeechFeedback, ValueTask> speechFeedbackHandler, string eventName = null);
    }
}
