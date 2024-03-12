using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers;

namespace aisha_ai.Services.SpeechServices.Foudations.SpeechFeedbackCheckers
{
    public class SpeechFeedbackCheckerService : ISpeechFeedbackCheckerService
    {
        private readonly IStorageBroker storageBroker;

        public SpeechFeedbackCheckerService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<SpeechFeedbackChecker> AddSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker) =>
          await storageBroker.InsertSpeechFeedbackCheckerAsync(speechFeedbackChecker);

        public IQueryable<SpeechFeedbackChecker> RetrieveAllSpeechFeedbackCheckers() =>
          storageBroker.SelectAllSpeechFeedbackCheckers();

        public async ValueTask<SpeechFeedbackChecker> ModifySpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker) =>
           await storageBroker.UpdateSpeechFeedbackCheckerAsync(speechFeedbackChecker);
    }
}
