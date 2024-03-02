using System;
using System.Threading.Tasks;
using aisha_ai.Models.ImageMetadatas;

namespace aisha_ai.Services.Foundations.ImageMetadataEvents
{
    public interface IImageMeatadataEventService
    {
        ValueTask PublishImageMetadataEventAsync(ImageMetadata imageMetadata, string eventName = null);
        void ListenToImageMetadataEvent(Func<ImageMetadata, ValueTask> imageMetadataHandler, string eventName = null);
    }
}
