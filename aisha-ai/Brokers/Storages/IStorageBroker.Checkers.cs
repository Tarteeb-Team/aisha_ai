using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Chekers;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public IQueryable<Checker> RetrieveAllCheckers();
        public ValueTask<Checker> InsertCheckerAsync(Checker checker);
        public ValueTask<Checker> DeleteCheckerAsync(Checker checker);
        ValueTask<Checker> UpdateCheckerAsync(Checker checker);
    }
}
