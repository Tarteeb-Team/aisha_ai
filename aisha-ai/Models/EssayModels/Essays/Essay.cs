using System;

namespace aisha_ai.Models.EssayModels.Essays
{
    public class Essay
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string TelegramUserName { get; set; }
        public Guid TelegramUserId { get; set; }
    }
}
