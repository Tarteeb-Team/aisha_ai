using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeeches;

namespace aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeeches
{
    public interface IImprovedSpeechService
    {
        ValueTask<ImprovedSpeech> AddImprovedSpeechAsync(ImprovedSpeech improvedSpeech);
        IQueryable<ImprovedSpeech> RetrieveAllImprovedSpeechs();
        ValueTask<ImprovedSpeech> RemoveImprovedSpeechAsync(ImprovedSpeech improvedSpeech);
        ValueTask<ImprovedSpeech> ModifyImprovedSpeechAsync(ImprovedSpeech improvedSpeech);
    }
}
