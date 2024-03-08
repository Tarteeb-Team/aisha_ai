using aisha_ai.Models.EssayModels.FeedbackCheckers;
using aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<ImprovedSpeechChecker> ImprovedSpeechCheckers { get; set; }

        public IQueryable<ImprovedSpeechChecker> RetrieveAllImprovedSpeechCheckers() =>
            SelectAll<ImprovedSpeechChecker>();

        public async ValueTask<ImprovedSpeechChecker> InsertImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker) =>
            await InsertAsync(improvedSpeechChecker);

        public async ValueTask<ImprovedSpeechChecker> UpdateImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker) =>
            await UpdateAsync(improvedSpeechChecker);
    }
}
