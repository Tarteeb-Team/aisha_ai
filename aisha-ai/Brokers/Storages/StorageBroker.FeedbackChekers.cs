using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.FeedbackCheckers;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<FeedbackChecker> FeedbackCheckerAnalyses { get; set; }

        public IQueryable<FeedbackChecker> RetrieveAllFeedbackCheckers() =>
            SelectAll<FeedbackChecker>();

        public async ValueTask<FeedbackChecker> InsertFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
            await InsertAsync(feedbackChecker);

        public async ValueTask<FeedbackChecker> DeleteFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
            await DeleteAsync(feedbackChecker);

        public async ValueTask<FeedbackChecker> UpdateFeedbackCheckerAsync(FeedbackChecker feedbackChecker) =>
            await UpdateAsync(feedbackChecker);
    }
}
