using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Essays;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages;

public partial class StorageBroker
{
    public DbSet<Essay> EssayAnalyses { get; set; }

    public IQueryable<Essay> SelectAllEssays() =>
        SelectAll<Essay>();

    public async ValueTask<Essay> InsertEssayAsync(Essay essay) =>
        await InsertAsync(essay);

    public async ValueTask<Essay> DeleteEssayAsync(Essay essay) =>
        await DeleteAsync(essay);

    public async ValueTask<Essay> UpdateEssayAsync(Essay essay) =>
        await UpdateAsync(essay);
}