using System;

namespace aisha_ai_speech.Models.FeedbackChecker
{
    public class FeedbackChecker
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string TelegramUserName { get; set; }
    }
}
