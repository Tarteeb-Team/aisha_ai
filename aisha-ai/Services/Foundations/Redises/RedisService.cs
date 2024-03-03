using System.Threading.Tasks;
using aisha_ai.Brokers.Redises;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Services.Foundations.Redises
{
    public class RedisService : IRedisService
    {
        private readonly IRedisBroker redisBroker;

        public RedisService(IRedisBroker redisBroker) => 
            this.redisBroker = redisBroker;

        public async ValueTask<TelegramUserStatus?> GetUserStatusAsync(string telegramUserId) =>
         await this.redisBroker.GetUserStatusAsync(telegramUserId);

        public async ValueTask SetUserStatusAsync(string telegramUserId, TelegramUserStatus status) =>
            await this.redisBroker.SetUserStatusAsync(telegramUserId, status);
    }
}
