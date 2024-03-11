using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechFeedback;
using LeVent.Clients;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<SpeechFeedback> SpeechFeedbackEvents { get; set; }

        public ValueTask PublishSpeechFeedbackAsync(SpeechFeedback speechFeedbackEvent, string eventName = null) =>
            this.SpeechFeedbackEvents.PublishEventAsync(speechFeedbackEvent, eventName);

        public void ListenToSpeechFeedback(Func<SpeechFeedback, ValueTask> speechFeedbackHandler, string eventName = null) =>
            this.SpeechFeedbackEvents.RegisterEventHandler(speechFeedbackHandler, eventName);
    }
}
