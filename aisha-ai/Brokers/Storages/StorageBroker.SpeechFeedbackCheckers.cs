using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<SpeechFeedbackChecker> SpeechFeedbackCheckers { get; set; }

        public async ValueTask<SpeechFeedbackChecker> InsertSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker) =>
            await InsertAsync(speechFeedbackChecker);

        public IQueryable<SpeechFeedbackChecker> SelectAllSpeechFeedbackCheckers() =>
            SelectAll<SpeechFeedbackChecker>();

        public async ValueTask<SpeechFeedbackChecker> DeleteSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker) =>
            await DeleteAsync(speechFeedbackChecker);

        public async ValueTask<SpeechFeedbackChecker> UpdateSpeechFeedbackCheckerAsync(SpeechFeedbackChecker speechFeedbackChecker) =>
            await UpdateAsync(speechFeedbackChecker);
    }
}
