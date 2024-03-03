using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;

namespace aisha_ai.Services.Orchestrations.TelegramStates
{
    public interface ITelegramStateOrchestrationService
    {
        ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage);
    }
}
