using System.Threading.Tasks;

namespace aisha_ai.Services.SpeechServices.SendToTelegramMessages
{
    public interface ISendSpeechToTelegramMessageOrcherstrationService
    {
        ValueTask SendToTelegramSpeechOverralMessageAsync(string telegramUserName);
    }
}
