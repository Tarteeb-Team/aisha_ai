using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.Essays;

namespace aisha_ai.Services.Foundations.Essays;

public class EssayService : IEssayService
{
    private readonly IStorageBroker storageBroker;

    public EssayService(IStorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }

    public async ValueTask<Essay> AddEssayAsync(Essay essay) =>
      await storageBroker.InsertEssayAsync(essay);

    public async ValueTask<Essay> RemoveEssayAsync(Essay essay) =>
       await storageBroker.DeleteEssayAsync(essay);

    public async ValueTask<Essay> ModifyEssayAsync(Essay essay) =>
       await storageBroker.UpdateEssayAsync(essay);

    public IQueryable<Essay> RetrieveAllEssays() =>
      storageBroker.RetrieveAllEssays();
}