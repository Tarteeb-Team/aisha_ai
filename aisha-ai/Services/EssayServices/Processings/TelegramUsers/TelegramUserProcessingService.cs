using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.TelegramUsers;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.Processings.TelegramUsers;

namespace aisha_ai.Services.EssayServices.Processings.TelegramUsers
{
    public class TelegramUserProcessingService : ITelegramUserProcessingService
    {
        private readonly ITelegramUserService telegramUserService;

        public TelegramUserProcessingService(ITelegramUserService telegramUserService) =>
            this.telegramUserService = telegramUserService;

        public async ValueTask<TelegramUser> EnsureTelegramUserAsync(TelegramUser telegramUser)
        {
            var existingUser = telegramUserService
                .RetrieveAllTelegramUsers()
                .FirstOrDefault(user => user.TelegramId == telegramUser.TelegramId);

            return existingUser == null ? await AddAndReturnTelegramUserAsync(telegramUser) : existingUser;
        }

        private async ValueTask<TelegramUser> AddAndReturnTelegramUserAsync(TelegramUser telegramUser)
        {
            return await telegramUserService.AddTelegramUserAsync(telegramUser);
        }

    }
}
