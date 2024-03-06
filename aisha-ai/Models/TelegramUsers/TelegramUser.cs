using System;
using System.Collections;
using aisha_ai.Models.Chekers;
using aisha_ai.Models.FeedbackCheckers;

namespace aisha_ai.Models.TelegramUsers
{
    public class TelegramUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long TelegramId { get; set; }
        public string TelegramUserName { get; set; }
        public TelegramUserStatus TelegramUserStatus { get; set; }
        public Guid CheckerId { get; set; }
        public Guid FeedbackCheckerId { get; set; }

        public Checker Checker { get; set; }
        public FeedbackChecker FeedbackChecker{ get; set; }
    }
}
