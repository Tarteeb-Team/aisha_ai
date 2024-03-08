using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.ImprovedEssays;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<ImprovedEssay> InsertImprovedEssayAsync(ImprovedEssay improvedEssay);
        IQueryable<ImprovedEssay> RetrieveAllImprovedEssays();
        ValueTask<ImprovedEssay> UpdateImprovedEssayAsync(ImprovedEssay improvedEssay);
        ValueTask<ImprovedEssay> DeleteImprovedEssayAsync(ImprovedEssay improvedEssay);
    }
}
