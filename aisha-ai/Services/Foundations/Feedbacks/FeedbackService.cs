using System;
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
       throw new NotImplementedException();

    public IQueryable<Feedback> RetrieveAllFeedbacks() =>
       throw new NotImplementedException();

    public ValueTask<Feedback> RemoveFeedbackAsync(Feedback feedback) =>
        throw new NotImplementedException();

    public ValueTask<Feedback> ModifyFeedbackAsync(Feedback feedback) =>
       throw new NotImplementedException();
}