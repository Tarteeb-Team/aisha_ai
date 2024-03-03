using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.TelegramUsers;
using LeVent.Brokers.Storages;

namespace aisha_ai.Services.Foundations.TelegramUsers;

public class TelegramUserService : ITelegramUserService
{
    private readonly IStorageBroker storageBroker;

    public TelegramUserService(IStorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }

    public async ValueTask<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser) =>
        await this.storageBroker.InsertTelegramUserAsync(telegramUser);
    
    public async ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser) =>
        await this.storageBroker.UpdateTelegramUserAsync(telegramUser);

    public IQueryable<TelegramUser> RetrieveAllTelegramUsers() =>
        this.storageBroker.SelectAllTelegramUsers();
}