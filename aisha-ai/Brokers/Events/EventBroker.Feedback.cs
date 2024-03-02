using aisha_ai.Models.EssayEvents;
using LeVent.Clients;
using System.Threading.Tasks;
using System;
using aisha_ai.Models.Feedbacks;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<Feedback> FeedbackEvents { get; set; }

        public ValueTask PublishFeedbackAsync(Feedback feedbackEvent, string eventName = null) =>
            this.FeedbackEvents.PublishEventAsync(feedbackEvent, eventName);

        public void ListenToFeedback(Func<Feedback, ValueTask> feedbackHandler, string eventName = null) =>
            this.FeedbackEvents.RegisterEventHandler(feedbackHandler, eventName);
    }
}
