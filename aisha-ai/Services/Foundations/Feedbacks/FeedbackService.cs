using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Services.Foundations.Feedbacks;

public class FeedbackService : IFeedbackService
{
    private readonly IStorageBroker storageBroker;

    public FeedbackService(IStorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }

    public ValueTask<Feedback> AddFeedbackAsync(Feedback feedback) =>
       this.storageBroker.InsertFeedbackAsync(feedback);

    public IQueryable<Feedback> RetrieveAllFeedbacks() =>
        this.storageBroker.SelectAllFeedbacks();

    public ValueTask<Feedback> RemoveFeedbackAsync(Feedback feedback) =>
        this.storageBroker.DeleteFeedbackAsync(feedback);

    public ValueTask<Feedback> ModifyFeedbackAsync(Feedback feedback) =>
        this.storageBroker.UpdateFeedbackAsync(feedback);
}