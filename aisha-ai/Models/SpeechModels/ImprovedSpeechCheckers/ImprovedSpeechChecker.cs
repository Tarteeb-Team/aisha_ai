using System;

namespace aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers
{
    public class ImprovedSpeechChecker
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public string TelegramUserName { get; set; }
    }
}
