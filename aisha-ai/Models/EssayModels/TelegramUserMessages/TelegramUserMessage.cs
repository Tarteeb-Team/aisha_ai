using aisha_ai.Models.EssayModels.TelegramUsers;
using Telegram.Bot.Types;

namespace aisha_ai.Models.EssayModels.TelegramUserMessages
{
    public class TelegramUserMessage
    {
        public TelegramUser TelegramUser { get; set; }
        public Message Message { get; set; }
    }
}
