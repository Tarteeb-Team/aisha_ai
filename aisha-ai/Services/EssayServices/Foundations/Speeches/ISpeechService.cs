using System.Threading.Tasks;

namespace aisha_ai.Services.EssayServices.Foundations.Speeches
{
    public interface ISpeechService
    {
        ValueTask<string> CreateAndSaveSpeechAudioAsync(string text, string fileName);
    }
}
