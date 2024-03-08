using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.TelegramUserMessages;

namespace aisha_ai.Services.Orchestrations.TelegramStates
{
    public interface ITelegramStateOrchestrationService
    {
        ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage);
    }
}
