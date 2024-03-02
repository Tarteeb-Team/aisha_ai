using System.Threading.Tasks;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Services.Orchestrations.FeedbackToSpeeches
{
    public interface IFeedbackToSpeechOrcherstrationService
    {
        void ListenToFeedback();
    }
}
