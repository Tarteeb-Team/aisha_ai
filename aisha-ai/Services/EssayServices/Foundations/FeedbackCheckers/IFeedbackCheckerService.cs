using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.FeedbackCheckers;

namespace aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers
{
    public interface IFeedbackCheckerService
    {
        public ValueTask<FeedbackChecker> AddFeedbackCheckerAsync(FeedbackChecker feedbackFeedbackChecker);
        public IQueryable<FeedbackChecker> RetrieveAllFeedbackCheckers();
        public ValueTask<FeedbackChecker> RemoveFeedbackCheckerAsync(FeedbackChecker feedbackFeedbackChecker);
        ValueTask<FeedbackChecker> ModifyFeedbackCheckerAsync(FeedbackChecker feedbackFeedbackChecker);
    }
}
