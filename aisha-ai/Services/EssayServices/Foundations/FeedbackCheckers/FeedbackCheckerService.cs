using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using RESTFulSense.Models;

namespace aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers
{
    public class FeedbackCheckerService : IFeedbackCheckerService
    {
        private readonly IStorageBroker storageBroker;

        public FeedbackCheckerService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<FeedbackChecker> AddFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
          await storageBroker.InsertFeedbackCheckerAsync(feedbackChecker);

        public async ValueTask<FeedbackChecker> RemoveFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
           await storageBroker.DeleteFeedbackCheckerAsync(feedbackChecker);

        public IQueryable<FeedbackChecker> RetrieveAllFeedbackCheckers() =>
          storageBroker.RetrieveAllFeedbackCheckers();

        public async ValueTask<FeedbackChecker> ModifyFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
            await storageBroker.UpdateFeedbackCheckerAsync(feedbackChecker);
    }
}
