using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.Transcriptions;
using aisha_ai.Services.SpeechServices.Orcherstrations.ImprovedSpeeches;
using aisha_ai.Services.SpeechServices.Orcherstrations.SpeechFeedbacks;

namespace aisha_ai.Services.SpeechServices.Coordinations.Speeches
{
    public class SpeechCoordinationService : ISpeechCoordinationService
    {
        private readonly ISpeechFeedbackOrcherstrationService speechFeedbackOrcherstrationService;
        private readonly IImprovedSpeechOrchestrationService improvedSpeechOrchestrationService;

        public SpeechCoordinationService(
            ISpeechFeedbackOrcherstrationService speechFeedbackOrcherstrationService,
            IImprovedSpeechOrchestrationService improvedSpeechOrchestrationService)
        {
            this.speechFeedbackOrcherstrationService = speechFeedbackOrcherstrationService;
            this.improvedSpeechOrchestrationService = improvedSpeechOrchestrationService;
        }

        public void ListenToTranscription()
        {
            this.speechFeedbackOrcherstrationService
                .ListenToSpeechFeedback(ProcessTranscriptionAsync);
        }

        private async ValueTask ProcessTranscriptionAsync(Transcription transcription) =>
            await this.improvedSpeechOrchestrationService.ProcessImproveSpeechAsync(transcription);
    }
}
