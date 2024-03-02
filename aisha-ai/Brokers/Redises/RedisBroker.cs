using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Tarteeb_bot_test.Models.TelegramUsers;

namespace Tarteeb_bot_test.Brokers.Redises
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
