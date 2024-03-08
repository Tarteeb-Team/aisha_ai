using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Chekers;

namespace aisha_ai.Services.Foundations.Checkers
{
    public interface ICheckerService
    {
        public ValueTask<Checker> AddCheckerAsync(Checker checker);
        public IQueryable<Checker> RetrieveAllCheckers();
        public ValueTask<Checker> RemoveCheckerAsync(Checker checker);
        ValueTask<Checker> ModifyCheckerAsync(Checker checker);
    }
}
