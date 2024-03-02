using System.Threading.Tasks;
using aisha_ai.Models.EssayEvents;

namespace aisha_ai.Services.Orchestrations.ImprovedEssays
{
    public interface IImprovedEssayOrchestratioinService
    {
        void ListenEssayEvent();
    }
}
