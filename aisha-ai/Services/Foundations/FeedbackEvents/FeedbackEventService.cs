using aisha_ai.Brokers.Events;
using aisha_ai.Models.EssayEvents;
using System.Threading.Tasks;
using System;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Services.Foundations.FeedbackEvents
{
    public class FeedbackEventService : IFeedbackEventService
    {
        private readonly IEventBroker eventBroker;

        public FeedbackEventService(IEventBroker eventBroker) =>
            this.eventBroker = eventBroker;

        public ValueTask PublishFeedbackAsync(Feedback feedback, string eventName = null) =>
            this.eventBroker.PublishFeedbackAsync(feedback, eventName);

        public void ListenToFeedback(
            Func<Feedback, ValueTask> feedbackHandler,
            string eventName = null)
        {
            this.eventBroker.ListenToFeedback(feedbackHandler, eventName);
        }
    }
}
