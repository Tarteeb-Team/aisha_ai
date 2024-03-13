using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers;

namespace aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers
{
    public interface IImprovedSpeechCheckerService
    {
        ValueTask<ImprovedSpeechChecker> AddImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker);
        IQueryable<ImprovedSpeechChecker> RetrieveAllImprovedSpeechCheckers();
        ValueTask<ImprovedSpeechChecker> ModifyImprovedSpeechCheckerAsync(ImprovedSpeechChecker improvedSpeechChecker);

    }
}
