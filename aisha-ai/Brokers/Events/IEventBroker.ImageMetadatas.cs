using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.ImageMetadatas;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        ValueTask PublishImageMetadataEventAsync(ImageMetadata imageMetadata, string eventName = null);
        void ListenToImageMetadataEvent(Func<ImageMetadata, ValueTask> imageMetadataHandler, string eventName = null);
    }
}
