using System;

namespace aisha_ai.Models.TelegramUsers
{
    public class TelegramUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long TelegramId { get; set; }
        public string TelegramUserName { get; set; }
        public TelegramUserStatus TelegramUserStatus { get; set; }
    }
}
