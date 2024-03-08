using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.EssayEvents;

namespace aisha_ai.Brokers.Events
{
    public partial interface IEventBroker
    {
        void PublishEssayEventAsync(EssayEvent essay, string eventName = null);
        void ListenToEssayEvent(Func<EssayEvent, ValueTask> essayHandler, string eventName = null);
    }
}
