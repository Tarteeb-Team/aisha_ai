using System;

namespace aisha_ai.Models.Chekers
{
    public class Checker
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string TelegramUserName { get; set; }
    }
}
