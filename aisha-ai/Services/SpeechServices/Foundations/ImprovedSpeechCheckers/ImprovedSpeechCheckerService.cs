using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers;

namespace aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechCheckers
{
    public class ImprovedSpeechCheckerService : IImprovedSpeechCheckerService
    {
        private readonly IStorageBroker storageBroker;

        public ImprovedSpeechCheckerService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public ValueTask<ImprovedSpeechChecker> AddImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker) =>
            this.storageBroker.InsertImprovedSpeechCheckerAsync(improvedSpeechChecker);

        public ValueTask<ImprovedSpeechChecker> ModifyImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker) =>
            this.storageBroker.UpdateImprovedSpeechCheckerAsync(improvedSpeechChecker);

        public IQueryable<ImprovedSpeechChecker> RetrieveAllImprovedSpeechCheckers() =>
            this.storageBroker.RetrieveAllImprovedSpeechCheckers();
    }
}
