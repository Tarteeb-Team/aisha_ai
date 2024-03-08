using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public IQueryable<PhotoChecker> RetrieveAllPhotoCheckers();
        public ValueTask<PhotoChecker> InsertPhotoCheckerAsync(PhotoChecker photoChecker);
        public ValueTask<PhotoChecker> DeletePhotoCheckerAsync(PhotoChecker photoChecker);
        ValueTask<PhotoChecker> UpdatePhotoCheckerAsync(PhotoChecker photoChecker);
    }
}
