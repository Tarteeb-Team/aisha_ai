using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.SpeechModels.ImprovedSpeeches;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<ImprovedSpeech> ImprovedSpeeches { get; set; }

        public async ValueTask<ImprovedSpeech> InsertImprovedSpeechAsync(ImprovedSpeech improvedSpeech) =>
            await InsertAsync(improvedSpeech);

        public IQueryable<ImprovedSpeech> SelectAllImprovedSpeeches() =>
            SelectAll<ImprovedSpeech>();

        public async ValueTask<ImprovedSpeech> DeleteImprovedSpeechAsync(ImprovedSpeech improvedSpeech) =>
            await DeleteAsync(improvedSpeech);
    }
}
