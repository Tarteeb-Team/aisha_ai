using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Services.Processings.TelegramUsers
{
    public interface ITelegramUserProcessingService
    {
        ValueTask<TelegramUser> EnsureTelegramUserAsync(TelegramUser telegramUser);
    }
}
