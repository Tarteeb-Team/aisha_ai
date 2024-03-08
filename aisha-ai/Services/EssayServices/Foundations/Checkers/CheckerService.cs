using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.Chekers;

namespace aisha_ai.Services.Foundations.Checkers
{
    public class CheckerService : ICheckerService
    {
        private readonly IStorageBroker storageBroker;

        public CheckerService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Checker> AddCheckerAsync(Checker checker) =>
          await this.storageBroker.InsertCheckerAsync(checker);

        public async ValueTask<Checker> RemoveCheckerAsync(Checker checker) =>
           await this.storageBroker.DeleteCheckerAsync(checker);

        public IQueryable<Checker> RetrieveAllCheckers() =>
          this.storageBroker.RetrieveAllCheckers();

        public async ValueTask<Checker> ModifyCheckerAsync(Checker checker) =>
            await this.storageBroker.UpdateCheckerAsync(checker);
    }
}
