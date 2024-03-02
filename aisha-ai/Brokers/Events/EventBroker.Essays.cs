using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayEvents;
using aisha_ai.Models.Essays;
using LeVent.Clients;

namespace aisha_ai.Brokers.Events
{
    public partial class EventBroker
    {
        public ILeVentClient<EssayEvent> EssayEvents { get; set; }

        public void PublishEssayEventAsync(EssayEvent essayEvent, string eventName = null) =>
            this.EssayEvents.PublishEventAsync(essayEvent, eventName);

        public void ListenToEssayEvent(Func<EssayEvent, ValueTask> essayHandler, string eventName = null) =>
            this.EssayEvents.RegisterEventHandler(essayHandler, eventName);
    }
}
