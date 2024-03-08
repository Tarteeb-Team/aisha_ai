using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Chekers;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Checker> CheckerAnalyses { get; set; }

        public IQueryable<Checker> RetrieveAllCheckers() =>
            SelectAll<Checker>();

        public async ValueTask<Checker> InsertCheckerAsync(Checker checker) =>
            await InsertAsync(checker);

        public async ValueTask<Checker> DeleteCheckerAsync(Checker checker) =>
            await DeleteAsync(checker);

        public async ValueTask<Checker> UpdateCheckerAsync(Checker checker) =>
            await UpdateAsync(checker);
    }
}
