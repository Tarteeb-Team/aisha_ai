using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Services.Foundations.Feedbacks;

namespace aisha_ai.Services.EssayServices.Foundations.Feedbacks;

public class FeedbackService : IFeedbackService
{
    private readonly IStorageBroker storageBroker;

    public FeedbackService(IStorageBroker storageBroker) =>
        this.storageBroker = storageBroker;

    public async ValueTask<Feedback> AddFeedbackAsync(Feedback feedback) =>
       await storageBroker.InsertFeedbackAsync(feedback);

    public IQueryable<Feedback> RetrieveAllFeedbacks() =>
       storageBroker.SelectAllFeedbacks();

    public async ValueTask<Feedback> RemoveFeedbackAsync(Feedback feedback) =>
        await storageBroker.DeleteFeedbackAsync(feedback);

    public async ValueTask<Feedback> ModifyFeedbackAsync(Feedback feedback) =>
       await storageBroker.UpdateFeedbackAsync(feedback);
}