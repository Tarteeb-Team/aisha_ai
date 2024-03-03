using aisha_ai.Models.TelegramUsers;
using Telegram.Bot.Types;

namespace aisha_ai.Models.TelegramUserMessages
{
    public class TelegramUserMessage
    {
        public TelegramUser TelegramUser { get; set; }
        public Message Message { get; set; }
    }
}
