using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;

namespace aisha_ai.Services.SpeechServices.Foudations.PronunciationAssessments
{
    public interface IPronunciationAssessmentService
    {
        ValueTask<SpeechFeedback> GetSpeechFeedbackAsync(string filePath, string telegramUserName);
    }
}
