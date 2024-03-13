using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;

namespace aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbacks
{
    public class SpeechFeedbackService : ISpeechFeedbackService
    {
        private readonly IStorageBroker storageBroker;

        public SpeechFeedbackService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<SpeechFeedback> AddSpeechFeedbackAsync(SpeechFeedback speechFeedback) =>
           await storageBroker.InsertSpeechFeedbackAsync(speechFeedback);

        public IQueryable<SpeechFeedback> RetrieveAllSpeechFeedbacks() =>
           storageBroker.SelectAllSpeechFeedbacks();

        public async ValueTask<SpeechFeedback> RemoveSpeechFeedbackAsync(SpeechFeedback speechFeedback) =>
            await storageBroker.DeleteSpeechFeedbackAsync(speechFeedback);
    }
}
