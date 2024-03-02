using aisha_ai.Models.EssayEvents;
using aisha_ai.Models.Feedbacks;
using aisha_ai.Models.ImageMetadatas;
using LeVent.Clients;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker : IEventBroker
    {
        public EventBroker()
        {
            this.ImageMetadataEvents = new LeVentClient<ImageMetadata>();
            this.EssayEvents = new LeVentClient<EssayEvent>();
            this.FeedbackEvents = new LeVentClient<Feedback>();
        }
    }
}
