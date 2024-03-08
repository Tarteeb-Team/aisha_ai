using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages;

public partial class StorageBroker
{
    public DbSet<Feedback> Feedback { get; set; }

    public async ValueTask<Feedback> InsertFeedbackAsync(Feedback feedback) =>
        await InsertAsync(feedback);

    public IQueryable<Feedback> SelectAllFeedbacks() =>
        SelectAll<Feedback>();

    public async ValueTask<Feedback> DeleteFeedbackAsync(Feedback feedback) =>
        await DeleteAsync(feedback);

    public async ValueTask<Feedback> UpdateFeedbackAsync(Feedback feedback) =>
        await UpdateAsync(feedback);
}