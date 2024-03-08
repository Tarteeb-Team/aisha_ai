using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.SpeechModels.SpeechFeedback;
using System.Linq;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<SpeechFeedback> InsertSpeechFeedbackAsync(SpeechFeedback speechFeedback);
        IQueryable<SpeechFeedback> SelectAllSpeechFeedbacks();
        ValueTask<SpeechFeedback> DeleteSpeechFeedbackAsync(SpeechFeedback speechFeedback);
    }
}
