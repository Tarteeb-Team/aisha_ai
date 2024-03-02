using System;
using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUsers;

namespace Tarteeb_bot_test.Brokers.Redises
{
    public interface IRedisBroker
    {
        ValueTask SetUserStatusAsync(string telegramUserId, TelegramUserStatus status);
        ValueTask<TelegramUserStatus?> GetUserStatusAsync(string telegramUserId);
    }
}
