using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUsers;

namespace Tarteeb_bot_test.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TelegramUser> TelegramUsers { get; set; }

        public async ValueTask<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser) =>
            await InsertAsync(telegramUser);
        public IQueryable<TelegramUser> SelectAllTelegramUsers() =>
            SelectAll<TelegramUser>();
        public async ValueTask<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser) =>
            await UpdateAsync(telegramUser);
    }
}
