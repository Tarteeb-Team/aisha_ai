using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.SpeechInfos;

namespace aisha_ai.Services.Foundations.SpeechInfos;

public class SpeechInfoService : ISpeechInfoService
{

    private readonly IStorageBroker storageBroker;

    public SpeechInfoService(IStorageBroker storageBroker)
    {
        this.storageBroker = storageBroker;
    }

    public async ValueTask<SpeechInfo> AddSpeechInfoAsync(SpeechInfo speechInfo) =>
        await this.storageBroker.InsertSpeechInfoAsync(speechInfo);

    public IQueryable<SpeechInfo> RetrieveAllSpeechInfos() =>
        this.storageBroker.SelectAllSpeechInfos();

    public async ValueTask<SpeechInfo> ModifySpeechInfoAsync(SpeechInfo speechInfo) =>
        await this.storageBroker.UpdateSpeechInfoAsync(speechInfo);
}