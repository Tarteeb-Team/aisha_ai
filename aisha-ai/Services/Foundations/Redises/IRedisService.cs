using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Services.Foundations.Redises
{
    public interface IRedisService
    {
        ValueTask SetUserStatusAsync(string telegramUserId, TelegramUserStatus status);
        ValueTask<TelegramUserStatus?> GetUserStatusAsync(string telegramUserId);
    }
}
