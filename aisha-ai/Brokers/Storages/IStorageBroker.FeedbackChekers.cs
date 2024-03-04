using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.FeedbackCheckers;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public IQueryable<FeedbackChecker> RetrieveAllFeedbackCheckers();
        public ValueTask<FeedbackChecker> InsertFeedbackCheckerAsync(FeedbackChecker feedbackChecker);
        public ValueTask<FeedbackChecker> DeleteFeedbackCheckerAsync(FeedbackChecker feedbackChecker);
        ValueTask<FeedbackChecker> UpdateFeedbackCheckerAsync(FeedbackChecker feedbackChecker);
    }
}
