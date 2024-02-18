using System;

namespace Tarteeb_bot_test.Models.TelegramUsers
{
    public class TelegramUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long TelegramId { get; set; }
        public TelegramUserStatus TelegramUserStatus { get; set; }
    }
}
