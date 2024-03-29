using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.TelegramUsers;

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

    public async ValueTask<TelegramUser> RemoveTelegramUserAsync(string telegramUserName)
    {
        var maybeTelegramUser = this.storageBroker.SelectAllTelegramUsers()
            .FirstOrDefault(t => t.TelegramUserName == telegramUserName);

        return await this.storageBroker.DeleteTelegramUserAsync(maybeTelegramUser);
    }
}