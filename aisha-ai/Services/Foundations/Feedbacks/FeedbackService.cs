using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Services.Foundations.Feedbacks;

public class FeedbackService : IFeedbackService
{
    private readonly IStorageBroker storageBroker;

    public FeedbackService(IStorageBroker storageBroker) =>
        this.storageBroker = storageBroker;

    public async ValueTask<Feedback> AddFeedbackAsync(Feedback feedback) =>
       await this.storageBroker.InsertFeedbackAsync(feedback);

    public IQueryable<Feedback> RetrieveAllFeedbacks() =>
       this.storageBroker.SelectAllFeedbacks();

    public async ValueTask<Feedback> RemoveFeedbackAsync(Feedback feedback) =>
        await this.storageBroker.DeleteFeedbackAsync(feedback);

    public ValueTask<Feedback> ModifyFeedbackAsync(Feedback feedback) =>
       throw new NotImplementedException();
}