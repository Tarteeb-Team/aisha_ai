using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishFeedbackAsync(Feedback feedback, string eventName = null);
        void ListenToFeedback(Func<Feedback, ValueTask> feedbackHandler, string eventName = null);
    }
}
