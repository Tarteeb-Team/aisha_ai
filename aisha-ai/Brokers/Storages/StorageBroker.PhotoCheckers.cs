using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<PhotoChecker> PhotoCheckers { get; set; }

        public IQueryable<PhotoChecker> RetrieveAllPhotoCheckers() =>
            SelectAll<PhotoChecker>();

        public async ValueTask<PhotoChecker> InsertPhotoCheckerAsync(PhotoChecker photoPhotoChecker) =>
            await InsertAsync(photoPhotoChecker);

        public async ValueTask<PhotoChecker> DeletePhotoCheckerAsync(PhotoChecker photoPhotoChecker) =>
            await DeleteAsync(photoPhotoChecker);

        public async ValueTask<PhotoChecker> UpdatePhotoCheckerAsync(PhotoChecker photoPhotoChecker) =>
            await UpdateAsync(photoPhotoChecker);
    }
}
