namespace aisha_ai.Models.SpeechModels.PronunciationAssessments.PronunciationAssessments
{
    public class PronunciationAssessment
    {
        public decimal AccuracyScore { get; set; }
        public decimal FluencyScore { get; set; }
        public decimal ProsodyScore { get; set; }
        public decimal CompletenessScore { get; set; }
        public decimal PronScore { get; set; }
    }
}
