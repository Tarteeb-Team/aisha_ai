using System.Threading.Tasks;

namespace aisha_ai.Brokers.Cognitives
{
    public interface IPronunciationAssessmentBroker
    {
        ValueTask<string> GetSpeechFeedbackJsonStringAsync(string filePath);
    }
}
