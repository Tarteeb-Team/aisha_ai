using System.Threading.Tasks;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Tarteeb_bot_test.Models.TelegramUsers;
using Tarteeb_bot_test.Services.Foundations.Levents.TelegramEvents;
using Tarteeb_bot_test.Services.Foundations.Redises;
using Tarteeb_bot_test.Services.Foundations.Telegrams;

namespace Tarteeb_bot_test.Services.Orchestrations.TelegramStates
{
    public class TelegramStateOrchestrationService : ITelegramStateOrchestrationService
    {
        private readonly IRedisService redisService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramEventService telegramEventService;

        public TelegramStateOrchestrationService(
            IRedisService redisService,
            ITelegramService telegramService,
            ITelegramEventService telegramEventService)
        {
            this.redisService = redisService;
            this.telegramService = telegramService;
            this.telegramEventService = telegramEventService;
        }

        public async ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage)
        {
            TelegramUserStatus? telegramUserStatus =
                await this.redisService.GetUserStatusAsync($"{telegramUserMessage.TelegramUser.TelegramId}");

            if (telegramUserMessage.Message.Text is "/start")
            {
                await this.redisService.SetUserStatusAsync(
                    $"{telegramUserMessage.TelegramUser.TelegramId}",
                    TelegramUserStatus.Register);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "HI, send please your number");

                return;
            }
            if (telegramUserStatus is TelegramUserStatus.Register)
            {
                await this.redisService.SetUserStatusAsync(
                    $"{telegramUserMessage.TelegramUser.TelegramId}",
                    TelegramUserStatus.Menu);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Register");
            }
            if (telegramUserStatus is TelegramUserStatus.Menu)
            {
                await this.redisService.SetUserStatusAsync(
                    $"{telegramUserMessage.TelegramUser.TelegramId}",
                    TelegramUserStatus.Active);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Menu.");
            }
            if (telegramUserStatus is TelegramUserStatus.Active)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Active.");
            }
        }
    }
}
