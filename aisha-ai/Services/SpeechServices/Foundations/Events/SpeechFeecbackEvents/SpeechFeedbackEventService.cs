using System;
using System.Threading.Tasks;
using aisha_ai.Brokers.Events;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;

namespace aisha_ai.Services.SpeechServices.Foundations.Events.SpeechFeecbackEvents
{
    public class SpeechFeedbackEventService : ISpeechFeedbackEventService
    {
        private readonly IEventBroker eventBroker;

        public SpeechFeedbackEventService(IEventBroker eventBroker) =>
            this.eventBroker = eventBroker;

        public ValueTask PublishSpeechFeedbackAsync(SpeechFeedback speechFeedback, string eventName = null) =>
            eventBroker.PublishSpeechFeedbackAsync(speechFeedback, eventName);

        public void ListenToSpeechFeedback(
            Func<SpeechFeedback, ValueTask> speechFeedbackHandler,
            string eventName = null)
        {
            eventBroker.ListenToSpeechFeedback(speechFeedbackHandler, eventName);
        }
    }
}
