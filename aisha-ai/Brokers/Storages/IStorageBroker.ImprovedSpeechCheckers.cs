using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers;

namespace aisha_ai.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        public IQueryable<ImprovedSpeechChecker> RetrieveAllImprovedSpeechCheckers();
        public ValueTask<ImprovedSpeechChecker> InsertImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker);
        ValueTask<ImprovedSpeechChecker> UpdateImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker);
    }
}
