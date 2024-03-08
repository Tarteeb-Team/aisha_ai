using System;
using System.Threading.Tasks;
using aisha_ai.Brokers.Events;
using aisha_ai.Models.EssayModels.ImageMetadatas;

namespace aisha_ai.Services.Foundations.ImageMetadataEvents
{
    public class ImageMeatadataEventService : IImageMeatadataEventService
    {
        private readonly IEventBroker eventBroker;

        public ImageMeatadataEventService(IEventBroker eventBroker) =>
            this.eventBroker = eventBroker;

        public ValueTask PublishImageMetadataEventAsync(ImageMetadata imageMetadata, string eventName = null) =>
            this.eventBroker.PublishImageMetadataEventAsync(imageMetadata, eventName);

        public void ListenToImageMetadataEvent(
            Func<ImageMetadata, ValueTask> imageMetadataHandler,
            string eventName = null)
        {
            this.eventBroker.ListenToImageMetadataEvent(imageMetadataHandler, eventName);
        }
    }
}
