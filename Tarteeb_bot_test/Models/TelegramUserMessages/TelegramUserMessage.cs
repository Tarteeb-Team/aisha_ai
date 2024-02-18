using Tarteeb_bot_test.Models.TelegramUsers;
using Telegram.Bot.Types;

namespace Tarteeb_bot_test.Models.TelegramUserMessages
{
    public class TelegramUserMessage
    {
        public TelegramUser TelegramUser { get; set; }
        public Message Message { get; set; }
    }
}
