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

    public ValueTask<Essay> AddEssayAsync(Essay essay) =>
        throw new NotImplementedException();

    public IQueryable<Essay> RetrieveAllEssays()=>
        throw new NotImplementedException();

    public ValueTask<Essay> RemoveEssayAsync(Essay essay)=>
        throw new NotImplementedException();

    public ValueTask<Essay> ModifyEssayAsync(Essay essay)=>
        throw new NotImplementedException();
}