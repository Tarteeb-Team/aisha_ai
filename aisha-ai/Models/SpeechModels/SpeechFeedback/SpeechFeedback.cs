using System;

namespace aisha_ai.Models.SpeechModels.SpeechFeedback
{
    public class SpeechFeedback
    {
        public Guid Id { get; set; }
        public string Transcription { get; set; }
        public decimal AccurancyScore { get; set; }
        public decimal FluencyScore { get; set; }
        public decimal ProsodyScore { get; set; }
        public decimal CompletenessScore { get; set; }
        public decimal PronunciationScore { get; set; }
        public string TelegramUserName { get; set; }
    }
}
