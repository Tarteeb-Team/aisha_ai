using System.Threading.Tasks;
using Tarteeb_bot_test.Brokers.Redises;
using Tarteeb_bot_test.Models.TelegramUsers;

namespace Tarteeb_bot_test.Services.Foundations.Redises
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
