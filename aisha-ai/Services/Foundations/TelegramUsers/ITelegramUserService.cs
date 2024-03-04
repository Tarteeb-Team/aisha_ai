using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Services.Foundations.TelegramUsers;

public interface ITelegramUserService
{
    ValueTask<TelegramUser> AddTelegramUserAsync(TelegramUser telegramUser);
    ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser);
    IQueryable<TelegramUser> RetrieveAllTelegramUsers();
}