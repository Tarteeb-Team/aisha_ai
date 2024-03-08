using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;

namespace aisha_ai.Services.Foundations.Essays;

public interface IEssayService
{
    ValueTask<Essay> AddEssayAsync(Essay essay);
    IQueryable<Essay> RetrieveAllEssays();
    ValueTask<Essay> RemoveEssayAsync(Essay essay);
    ValueTask<Essay> ModifyEssayAsync(Essay essay);
}