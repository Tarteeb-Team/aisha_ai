using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Brokers.Redises
{
    public interface IRedisBroker
    {
        ValueTask SetUserStatusAsync(string telegramUserId, TelegramUserStatus status);
        ValueTask<TelegramUserStatus?> GetUserStatusAsync(string telegramUserId);
    }
}
