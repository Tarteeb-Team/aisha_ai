using System;
using System.Threading.Tasks;
using aisha_ai.Brokers.Events;
using aisha_ai.Models.EssayModels.EssayEvents;

namespace aisha_ai.Services.Foundations.EssayEvents
{
    public class EssayEventService : IEssayEventService
    {
        private readonly IEventBroker eventBroker;

        public EssayEventService(IEventBroker eventBroker) =>
            this.eventBroker = eventBroker;

        public void PublishEssayEventAsync(EssayEvent essay, string eventName = null) =>
            this.eventBroker.PublishEssayEventAsync(essay, eventName);

        public void ListenToEssayEvent(
            Func<EssayEvent, ValueTask> essayHandler,
            string eventName = null)
        {
            this.eventBroker.ListenToEssayEvent(essayHandler, eventName);
        }
    }
}
