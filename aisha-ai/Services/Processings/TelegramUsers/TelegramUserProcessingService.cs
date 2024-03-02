using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;
using aisha_ai.Services.Foundations.TelegramUsers;

namespace aisha_ai.Services.Processings.TelegramUsers
{
    public class TelegramUserProcessingService : ITelegramUserProcessingService
    {
        private readonly ITelegramUserService telegramUserService;

        public TelegramUserProcessingService(ITelegramUserService telegramUserService) =>
            this.telegramUserService = telegramUserService;

        public async ValueTask<TelegramUser> EnsureTelegramUserAsync(TelegramUser telegramUser)
        {
            var existingUser = this.telegramUserService
                .RetrieveAllTelegramUsers()
                .FirstOrDefault(user => user.TelegramId == telegramUser.TelegramId);

            return existingUser == null ? await AddAndReturnTelegramUserAsync(telegramUser) : existingUser;
        }

        private async ValueTask<TelegramUser> AddAndReturnTelegramUserAsync(TelegramUser telegramUser)
        {
            return await this.telegramUserService.AddTelegramUserAsync(telegramUser);
        }

    }
}
