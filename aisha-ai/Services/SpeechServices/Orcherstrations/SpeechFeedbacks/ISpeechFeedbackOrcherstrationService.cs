using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.Transcriptions;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.SpeechFeedbacks
{
    public interface ISpeechFeedbackOrcherstrationService
    {
        void ListenToSpeechFeedback(Func<Transcription, ValueTask> speechFeedbackHandler);
    }
}
