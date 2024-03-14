using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;

namespace aisha_ai.Services.SpeechServices.Foundations.Events.SpeechFeecbackEvents
{
    public interface ISpeechFeedbackEventService
    {
        ValueTask PublishSpeechFeedbackAsync(SpeechFeedback speechFeedback, string eventName = null);
        void ListenToSpeechFeedback(
            Func<SpeechFeedback, ValueTask> speechFeedbackHandler,
            string eventName = null);
    }
}
