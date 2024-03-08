using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.SpeechInfos;

namespace aisha_ai.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<SpeechInfo> InsertSpeechInfoAsync(SpeechInfo speechInfo);
    IQueryable<SpeechInfo> SelectAllSpeechInfos();
    ValueTask<SpeechInfo> UpdateSpeechInfoAsync(SpeechInfo speechInfo);
    ValueTask<SpeechInfo> DeleteSpeechInfoAsync(SpeechInfo speechInfo);
}