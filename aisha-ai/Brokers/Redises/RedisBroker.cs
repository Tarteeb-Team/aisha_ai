using System;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;
using Microsoft.Extensions.Caching.Distributed;

namespace aisha_ai.Brokers.Redises
{
    public class RedisBroker : IRedisBroker
    {
        private readonly IDistributedCache cache;

        public RedisBroker(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async ValueTask SetUserStatusAsync(string telegramUserId, TelegramUserStatus status)
        {
            try
            {
                await this.cache.SetStringAsync(telegramUserId, status.ToString());

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async ValueTask<TelegramUserStatus?> GetUserStatusAsync(string telegramUserId)
        {
            string userStatusJson = await this.cache.GetStringAsync(telegramUserId);
            if (!string.IsNullOrEmpty(userStatusJson))
            {
                return Enum.Parse<TelegramUserStatus>(userStatusJson);
            }
            return null;
        }
    }
}
