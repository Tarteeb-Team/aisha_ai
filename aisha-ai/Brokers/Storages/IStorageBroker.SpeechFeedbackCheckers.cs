using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers;
using System.Linq;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<SpeechFeedbackChecker> InsertSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker);
        IQueryable<SpeechFeedbackChecker> SelectAllSpeechFeedbackCheckers();
        ValueTask<SpeechFeedbackChecker> UpdateSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker);
    }
}
