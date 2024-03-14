using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeeches;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<ImprovedSpeech> InsertImprovedSpeechAsync(ImprovedSpeech improvedSpeech);
        IQueryable<ImprovedSpeech> SelectAllImprovedSpeeches();
        ValueTask<ImprovedSpeech> DeleteImprovedSpeechAsync(ImprovedSpeech improvedSpeech);
        ValueTask<ImprovedSpeech> UpdateImprovedSpeechAsync(ImprovedSpeech improvedSpeech);
    }
}
