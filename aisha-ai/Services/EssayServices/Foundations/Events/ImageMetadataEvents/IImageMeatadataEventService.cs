using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.ImageMetadatas;

namespace aisha_ai.Services.EssayServices.Foundations.Events.ImageMetadataEvents
{
    public interface IImageMeatadataEventService
    {
        ValueTask PublishImageMetadataEventAsync(ImageMetadata imageMetadata, string eventName = null);
        void ListenToImageMetadataEvent(Func<ImageMetadata, ValueTask> imageMetadataHandler, string eventName = null);
    }
}
