using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.FeedbackCheckers;

namespace aisha_ai.Services.Foundations.FeedbackFeedbackCheckers
{
    public class FeedbackCheckerService : IFeedbackCheckerService
    {
        private readonly IStorageBroker storageBroker;

        public FeedbackCheckerService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<FeedbackChecker> AddFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
          await this.storageBroker.InsertFeedbackCheckerAsync(feedbackChecker);

        public async ValueTask<FeedbackChecker> RemoveFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
           await this.storageBroker.DeleteFeedbackCheckerAsync(feedbackChecker);

        public IQueryable<FeedbackChecker> RetrieveAllFeedbackCheckers() =>
          this.storageBroker.RetrieveAllFeedbackCheckers();

        public async ValueTask<FeedbackChecker> ModifyFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
            await this.storageBroker.UpdateFeedbackCheckerAsync(feedbackChecker);
    }
}
