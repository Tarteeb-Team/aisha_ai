using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;

namespace aisha_ai.Services.Foundations.PhotoCheckers
{
    public class PhotoCheckersService : IPhotoCheckersService
    {
        private readonly IStorageBroker storageBroker;

        public PhotoCheckersService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<PhotoChecker> AddPhotoCheckerAsync(PhotoChecker photoChecker) =>
          await this.storageBroker.InsertPhotoCheckerAsync(photoChecker);

        public async ValueTask<PhotoChecker> RemovePhotoCheckerAsync(PhotoChecker photoChecker) =>
           await this.storageBroker.DeletePhotoCheckerAsync(photoChecker);

        public IQueryable<PhotoChecker> RetrieveAllPhotoCheckers() =>
          this.storageBroker.RetrieveAllPhotoCheckers();

        public async ValueTask<PhotoChecker> ModifyPhotoCheckerAsync(PhotoChecker photoChecker) =>
            await this.storageBroker.UpdatePhotoCheckerAsync(photoChecker);
    }
}
