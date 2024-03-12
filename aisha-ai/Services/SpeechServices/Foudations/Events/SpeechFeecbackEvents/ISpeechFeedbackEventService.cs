using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechFeedback;

namespace aisha_ai.Services.SpeechServices.Foudations.Events.SpeechFeecbackEvents
{
    public interface ISpeechFeedbackEventService
    {
        ValueTask PublishSpeechFeedbackAsync(SpeechFeedback speechFeedback, string eventName = null);
        void ListenToSpeechFeedback(
            Func<SpeechFeedback, ValueTask> speechFeedbackHandler,
            string eventName = null);
    }
}
