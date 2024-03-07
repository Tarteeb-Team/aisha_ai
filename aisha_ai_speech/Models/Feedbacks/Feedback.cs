using System;

namespace aisha_ai_speech.Models.Feedbacks
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public decimal AccuracyScore { get; set; }
        public decimal FluencyScore { get; set; }
        public decimal ProsodyScore { get; set; }
        public decimal CompletenessScore { get; set; }
        public decimal PronunciationScore { get; set; }
        public string TelegramUserName { get; set; }
    }
}
