using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
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
