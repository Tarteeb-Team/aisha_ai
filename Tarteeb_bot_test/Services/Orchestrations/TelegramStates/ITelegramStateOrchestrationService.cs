using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUserMessages;

namespace Tarteeb_bot_test.Services.Orchestrations.TelegramStates
{
    public interface ITelegramStateOrchestrationService
    {
        ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage);
    }
}
