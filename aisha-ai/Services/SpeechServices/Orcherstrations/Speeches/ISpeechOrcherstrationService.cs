using System.Threading.Tasks;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.Speeches
{
    public interface ISpeechOrcherstrationService
    {
        ValueTask SpeechProcessAsync(string blobName);
    }
}
