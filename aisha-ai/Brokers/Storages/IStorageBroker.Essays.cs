using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Essays;

namespace aisha_ai.Brokers.Storages;

public partial interface IStorageBroker
{
    public IQueryable<Essay> RetrieveAllEssays();
    public ValueTask<Essay> InsertEssayAsync(Essay essay);
    public ValueTask<Essay> DeleteEssayAsync(Essay essay);
    public ValueTask<Essay> UpdateEssayAsync(Essay essay);
}