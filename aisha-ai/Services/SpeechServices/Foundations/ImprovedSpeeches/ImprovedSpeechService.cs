using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.SpeechModels.ImprovedSpeeches;

namespace aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeeches
{
    public class ImprovedSpeechService : IImprovedSpeechService
    {
        private readonly IStorageBroker storageBroker;

        public ImprovedSpeechService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<ImprovedSpeech> AddImprovedSpeechAsync(ImprovedSpeech improvedSpeech) =>
           await storageBroker.InsertImprovedSpeechAsync(improvedSpeech);

        public IQueryable<ImprovedSpeech> RetrieveAllImprovedSpeechs() =>
           storageBroker.SelectAllImprovedSpeeches();

        public async ValueTask<ImprovedSpeech> RemoveImprovedSpeechAsync(ImprovedSpeech improvedSpeech) =>
            await storageBroker.DeleteImprovedSpeechAsync(improvedSpeech);

        public async ValueTask<ImprovedSpeech> ModifyImprovedSpeechAsync(ImprovedSpeech improvedSpeech) =>
            await storageBroker.UpdateImprovedSpeechAsync(improvedSpeech);
    }
}
