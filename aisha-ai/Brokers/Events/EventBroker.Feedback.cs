using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;
using LeVent.Clients;

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
