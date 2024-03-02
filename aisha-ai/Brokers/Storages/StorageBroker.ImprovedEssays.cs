using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.ImprovedEssays;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<ImprovedEssay> ImprovedEssays { get; set; }

        public async ValueTask<ImprovedEssay> InsertImprovedEssayAsync(ImprovedEssay improvedEssay) =>
            await InsertAsync(improvedEssay);

        public async ValueTask<ImprovedEssay> DeleteImprovedEssayAsync(ImprovedEssay improvedEssay) =>
            await DeleteAsync(improvedEssay);

        public async ValueTask<ImprovedEssay> UpdateImprovedEssayAsync(ImprovedEssay improvedEssay) =>
            await UpdateAsync(improvedEssay);

        public IQueryable<ImprovedEssay> RetrieveAllImprovedEssays() =>
            SelectAll<ImprovedEssay>();
    }
}
