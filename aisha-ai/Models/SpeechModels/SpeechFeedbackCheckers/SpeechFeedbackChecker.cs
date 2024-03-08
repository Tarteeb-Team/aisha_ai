using System;

namespace aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers
{
    public class SpeechFeedbackChecker
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string TelegramUserName { get; set; }
    }
}
