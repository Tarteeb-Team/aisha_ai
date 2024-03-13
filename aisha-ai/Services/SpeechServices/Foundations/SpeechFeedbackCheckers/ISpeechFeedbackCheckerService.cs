using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers;

namespace aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbackCheckers
{
    public interface ISpeechFeedbackCheckerService
    {
        ValueTask<SpeechFeedbackChecker> AddSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker);
        IQueryable<SpeechFeedbackChecker> RetrieveAllSpeechFeedbackCheckers();
        ValueTask<SpeechFeedbackChecker> ModifySpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker);
    }
}
