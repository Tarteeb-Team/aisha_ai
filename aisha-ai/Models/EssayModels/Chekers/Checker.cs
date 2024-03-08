using System;
using aisha_ai.Models.EssayModels.TelegramUsers;

namespace aisha_ai.Models.EssayModels.Chekers
{
    public class Checker
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string TelegramUserName { get; set; }
        public Guid TelegramUserId { get; set; }
        public TelegramUser TelegramUser { get; set; }
    }
}
