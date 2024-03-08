using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.SpeechInfos;

namespace aisha_ai.Services.Foundations.SpeechInfos;

public interface ISpeechInfoService
{
    ValueTask<SpeechInfo> AddSpeechInfoAsync(SpeechInfo speechInfo);
    IQueryable<SpeechInfo> RetrieveAllSpeechInfos();
    ValueTask<SpeechInfo> ModifySpeechInfoAsync(SpeechInfo speechInfo);
}