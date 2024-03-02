using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<Feedback> InsertFeedbackAsync(Feedback feedback);
    IQueryable<Feedback> SelectAllFeedbacks();
    ValueTask<Feedback> DeleteFeedbackAsync(Feedback feedback);
    ValueTask<Feedback> UpdateFeedbackAsync(Feedback feedback);
}