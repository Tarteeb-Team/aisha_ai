using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;
using aisha_ai.Models.TelegramUsers;
using aisha_ai.Services.Foundations.Levents.TelegramEvents;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Services.Orchestrations.TelegramStates
{
    public class TelegramStateOrchestrationService : ITelegramStateOrchestrationService
    {
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;
        private readonly ITelegramUserMessageEventService telegramUserMessageEventService;

        public TelegramStateOrchestrationService(
            ITelegramService telegramService,
            ITelegramUserMessageEventService telegramUserMessageEventService,
            ITelegramUserService telegramUserService)
        {
            this.telegramService = telegramService;
            this.telegramUserMessageEventService = telegramUserMessageEventService;
            this.telegramUserService = telegramUserService;
        }

        public async ValueTask DispatchProcessAsync(TelegramUserMessage telegramUserMessage)
        {
            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramId == telegramUserMessage.TelegramUser.TelegramId);
            
            if (telegramUserMessage.Message.Text is "/start")
            {
                if (telegramUser != null)
                {
                    telegramUser.TelegramUserStatus = TelegramUserStatus.Active;
                    await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);
                
                    await this.telegramService.SendMessageAsync(
                        userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                        replyMarkup: new ReplyKeyboardMarkup("Photo") { ResizeKeyboard = true },
                        message: "HI, you are already registered.");

                    return;
                }
            }
            if (telegramUserMessage.Message.Text is "Photo"
                && telegramUser?.TelegramUserStatus is TelegramUserStatus.Active)
            {
                telegramUser.TelegramUserStatus = TelegramUserStatus.Photo;
                await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Please, send essay.");

                return;
            }

            if (telegramUserMessage.Message.Type is MessageType.Photo
                && telegramUser?.TelegramUserStatus is TelegramUserStatus.Photo)
            {
                
            }
        }
    }
}
