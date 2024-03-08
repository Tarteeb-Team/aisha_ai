using System.Threading.Tasks;

namespace aisha_ai.Services.EssayServices.Foundations.Speeches
{
    public interface ISpeechService
    {
        ValueTask<string> SaveSpeechAudioAsync(string text, string telegramUserName);
    }
}
