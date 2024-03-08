using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.SpeechModels.SpeechFeedback;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
