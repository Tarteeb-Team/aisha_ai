using aisha_ai.Models.EssayModels.EssayEvents;
using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.EssayModels.ImageMetadatas;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
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
            this.SpeechFeedbackEvents = new LeVentClient<SpeechFeedback>();
        }
    }
}
