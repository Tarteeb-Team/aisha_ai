using aisha_ai.Models.Feedbacks;
using System.Threading.Tasks;
using System;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishFeedbackAsync(Feedback feedback, string eventName = null);
        void ListenToFeedback(Func<Feedback, ValueTask> feedbackHandler, string eventName = null);
    }
}
