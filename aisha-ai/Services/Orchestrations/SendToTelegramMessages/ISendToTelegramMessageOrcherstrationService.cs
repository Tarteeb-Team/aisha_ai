using System.Threading.Tasks;

namespace aisha_ai.Services.Orchestrations.SendToTelegramMessages
{
    public interface ISendToTelegramMessageOrcherstrationService
    {
        ValueTask SendToTelegramOverralMessageAsync(string telegramUserName);
    }
}
