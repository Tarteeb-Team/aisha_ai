using System.Threading.Tasks;

namespace aisha_ai.Services.EssayServices.Orchestrations.SendToTelegramMessages
{
    public interface ISendToTelegramMessageOrcherstrationService
    {
        ValueTask SendToTelegramEssayOverralMessageAsync(string telegramUserName);
    }
}
