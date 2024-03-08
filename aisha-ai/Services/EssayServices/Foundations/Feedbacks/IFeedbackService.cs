using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;

namespace aisha_ai.Services.Foundations.Feedbacks;

public interface IFeedbackService
{
    public ValueTask<Feedback> AddFeedbackAsync(Feedback feedback);
    public IQueryable<Feedback> RetrieveAllFeedbacks();
    public ValueTask<Feedback> RemoveFeedbackAsync(Feedback feedback);
    public ValueTask<Feedback> ModifyFeedbackAsync(Feedback feedback);
}