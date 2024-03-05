using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.FeedbackCheckers;

namespace aisha_ai.Services.Foundations.FeedbackFeedbackCheckers
{
    public interface IFeedbackCheckerService
    {
        public ValueTask<FeedbackChecker> AddFeedbackCheckerAsync(FeedbackChecker feedbackFeedbackChecker);
        public IQueryable<FeedbackChecker> RetrieveAllFeedbackCheckers();
        public ValueTask<FeedbackChecker> RemoveFeedbackCheckerAsync(FeedbackChecker feedbackFeedbackChecker);
        ValueTask<FeedbackChecker> ModifyFeedbackCheckerAsync(FeedbackChecker feedbackFeedbackChecker);
    }
}
