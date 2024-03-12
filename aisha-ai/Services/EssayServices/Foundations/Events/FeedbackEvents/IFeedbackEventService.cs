using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;

namespace aisha_ai.Services.EssayServices.Foundations.Events.FeedbackEvents
{
    public interface IFeedbackEventService
    {
        ValueTask PublishFeedbackAsync(Feedback feedback, string eventName = null);
        void ListenToFeedback(
            Func<Feedback, ValueTask> feedbackHandler,
            string eventName = null);
    }
}
