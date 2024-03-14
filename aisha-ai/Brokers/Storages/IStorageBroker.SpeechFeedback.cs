using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<SpeechFeedback> InsertSpeechFeedbackAsync(SpeechFeedback speechFeedback);
        IQueryable<SpeechFeedback> SelectAllSpeechFeedbacks();
        ValueTask<SpeechFeedback> DeleteSpeechFeedbackAsync(SpeechFeedback speechFeedback);
        ValueTask<SpeechFeedback> UpdateSpeechFeedbackAsync(SpeechFeedback speechFeedback);
    }
}
