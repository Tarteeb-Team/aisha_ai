using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.Transcriptions;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.ImprovedSpeeches
{
    public interface IImprovedSpeechOrchestrationService
    {
        ValueTask ProcessImproveSpeechAsync(Transcription transcription);
    }
}
