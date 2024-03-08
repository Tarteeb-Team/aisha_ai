using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;

namespace aisha_ai.Services.Foundations.PhotoCheckers
{
    public interface IPhotoCheckersService
    {
        public ValueTask<PhotoChecker> AddPhotoCheckerAsync(PhotoChecker checker);
        public IQueryable<PhotoChecker> RetrieveAllPhotoCheckers();
        public ValueTask<PhotoChecker> RemovePhotoCheckerAsync(PhotoChecker checker);
        ValueTask<PhotoChecker> ModifyPhotoCheckerAsync(PhotoChecker checker);
    }
}
