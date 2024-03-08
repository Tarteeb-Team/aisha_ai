using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;

namespace aisha_ai.Services.Orchestrations.Feedbacks
{
    public interface IFeedbackOrchestrationService
    {
        ValueTask ProcessFeedbackAsync(Essay essay);
    }
}
