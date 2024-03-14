using System;

namespace aisha_ai.Models.SpeechModels.SpeechesFeedback
{
    public class SpeechFeedback
    {
        public Guid Id { get; set; }
        public string Transcription { get; set; }
        public decimal AccuracyScore { get; set; }
        public decimal FluencyScore { get; set; }
        public decimal ProsodyScore { get; set; }
        public decimal CompletenessScore { get; set; }
        public decimal PronunciationScore { get; set; }
        public string TelegramUserName { get; set; }
    }
}
