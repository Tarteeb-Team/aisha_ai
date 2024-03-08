using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.SpeechInfos;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages;

public partial class StorageBroker
{
    public DbSet<SpeechInfo> SpeechesInfo { get; set; }

    public async ValueTask<SpeechInfo> InsertSpeechInfoAsync(SpeechInfo speechInfo) =>
        await InsertAsync(speechInfo);

    public IQueryable<SpeechInfo> SelectAllSpeechInfos() =>
        SelectAll<SpeechInfo>();

    public async ValueTask<SpeechInfo> UpdateSpeechInfoAsync(SpeechInfo speechInfo) =>
        await UpdateAsync(speechInfo);

    public async ValueTask<SpeechInfo> DeleteSpeechInfoAsync(SpeechInfo speechInfo) =>
        await DeleteAsync(speechInfo);
}