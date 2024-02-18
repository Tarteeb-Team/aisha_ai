using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUsers;

namespace Tarteeb_bot_test.Services.Foundations.Redises
{
    public interface IRedisService
    {
        ValueTask SetUserStatusAsync(string telegramUserId, TelegramUserStatus status);
        ValueTask<TelegramUserStatus?> GetUserStatusAsync(string telegramUserId);
    }
}
