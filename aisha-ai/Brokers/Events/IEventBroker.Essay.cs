using aisha_ai.Models.ImageMetadatas;
using System.Threading.Tasks;
using System;
using aisha_ai.Models.Essays;
using aisha_ai.Models.EssayEvents;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        void PublishEssayEventAsync(EssayEvent essay, string eventName = null);
        void ListenToEssayEvent(Func<EssayEvent, ValueTask> essayHandler, string eventName = null);
    }
}
