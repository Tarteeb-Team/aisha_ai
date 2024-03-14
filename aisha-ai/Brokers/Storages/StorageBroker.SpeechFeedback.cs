using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<SpeechFeedback> SpeechFeedback { get; set; }

        public async ValueTask<SpeechFeedback> InsertSpeechFeedbackAsync(SpeechFeedback speechFeedback) =>
            await InsertAsync(speechFeedback);

        public IQueryable<SpeechFeedback> SelectAllSpeechFeedbacks() =>
            SelectAll<SpeechFeedback>();

        public async ValueTask<SpeechFeedback> DeleteSpeechFeedbackAsync(SpeechFeedback speechFeedback) =>
            await DeleteAsync(speechFeedback);

        public async ValueTask<SpeechFeedback> UpdateSpeechFeedbackAsync(SpeechFeedback speechFeedback) =>
            await UpdateAsync(speechFeedback);
    }
}
