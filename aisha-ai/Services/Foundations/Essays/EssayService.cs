using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.Essays;

namespace aisha_ai.Services.Foundations.Essays;

public class EssayService : IEssayService
{
    private readonly IStorageBroker storageBroker;

    public EssayService(IStorageBroker storageBroker) =>
        this.storageBroker = storageBroker;

    public async ValueTask<Essay> AddEssayAsync(Essay essay) =>
        await this.storageBroker.InsertEssayAsync(essay);

    public IQueryable<Essay> RetrieveAllEssays() =>
        this.storageBroker.SelectAllEssays();

    public ValueTask<Essay> ModifyEssayAsync(Essay essay) =>
        throw new NotImplementedException();
    
    public ValueTask<Essay> RemoveEssayAsync(Essay essay) =>
        throw new NotImplementedException();
}