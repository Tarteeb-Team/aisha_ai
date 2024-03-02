using aisha_ai.Models.EssayEvents;
using System.Threading.Tasks;
using System;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Services.Foundations.FeedbackEvents
{
    public interface IFeedbackEventService
    {
        ValueTask PublishFeedbackAsync(Feedback feedback, string eventName = null);
        void ListenToFeedback(
            Func<Feedback, ValueTask> feedbackHandler,
            string eventName = null);
    }
}
