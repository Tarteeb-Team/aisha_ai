using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayEvents;

namespace aisha_ai.Services.Foundations.EssayEvents
{
    public interface IEssayEventService
    {
        void PublishEssayEventAsync(EssayEvent essay, string eventName = null);
        void ListenToEssayEvent(
            Func<EssayEvent, ValueTask> essayHandler,
            string eventName = null);
    }
}
