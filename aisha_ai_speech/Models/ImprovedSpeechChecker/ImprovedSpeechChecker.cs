using System;

namespace aisha_ai_speech.Models.ImprovedSpeechChecker
{
    public class ImprovedSpeechChecker
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string TelegramUserName { get; set; }
    }
}
