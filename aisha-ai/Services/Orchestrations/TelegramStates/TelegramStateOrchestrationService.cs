using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;
using aisha_ai.Models.TelegramUsers;
using aisha_ai.Services.Foundations.Levents.TelegramEvents;
using aisha_ai.Services.Foundations.Redises;
using aisha_ai.Services.Foundations.Telegrams;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Services.Orchestrations.TelegramStates
{
    public class TelegramStateOrchestrationService : ITelegramStateOrchestrationService
    {
        private readonly IRedisService redisService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserMessageEventService telegramUserMessageEventService;

        public TelegramStateOrchestrationService(
            IRedisService redisService,
            ITelegramService telegramService,
            ITelegramUserMessageEventService telegramUserMessageEventService)
        {
            this.redisService = redisService;
            this.telegramService = telegramService;
            this.telegramUserMessageEventService = telegramUserMessageEventService;
        }

        public async ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage)
        {
            TelegramUserStatus? telegramUserStatus = await this.redisService
                .GetUserStatusAsync($"{telegramUserMessage.TelegramUser.TelegramId}");

            if (telegramUserMessage.Message.Text is "/start")
            {
                await this.redisService.SetUserStatusAsync(
                    $"{telegramUserMessage.TelegramUser.TelegramId}",
                    TelegramUserStatus.Menu);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    replyMarkup: new ReplyKeyboardMarkup("Test speech") { ResizeKeyboard = true },
                    message: "HI, you are already registered.");

                return;
            }
            if (telegramUserMessage.Message.Text is "Test speech"
                && telegramUserStatus is TelegramUserStatus.Menu)
            {
                await this.redisService.SetUserStatusAsync(
                    $"{telegramUserMessage.TelegramUser.TelegramId}",
                    TelegramUserStatus.TestSpeech);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Please, send voice.");
            }
        }
    }
}
