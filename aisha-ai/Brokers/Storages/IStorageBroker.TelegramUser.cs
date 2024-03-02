using System.Linq;
using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUsers;

namespace Tarteeb_bot_test.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser);
        IQueryable<TelegramUser> SelectAllTelegramUsers();
        ValueTask<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser);
    }
}
