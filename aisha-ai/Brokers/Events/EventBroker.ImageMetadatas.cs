using System;
using System.Threading.Tasks;
using aisha_ai.Models.ImageMetadatas;
using LeVent.Clients;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<ImageMetadata> ImageMetadataEvents { get; set; }

        public ValueTask PublishImageMetadataEventAsync(ImageMetadata imageMetadata, string eventName = null) =>
            this.ImageMetadataEvents.PublishEventAsync(imageMetadata, eventName);

        public void ListenToImageMetadataEvent(Func<ImageMetadata, ValueTask> imageMetadataHandler, string eventName = null) =>
            this.ImageMetadataEvents.RegisterEventHandler(imageMetadataHandler, eventName);
    }
}
