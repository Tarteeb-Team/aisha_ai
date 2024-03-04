using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.ImprovedEssays;

namespace aisha_ai.Services.Foundations.ImprovedEssays
{
    public class ImprovedEssayService : IImprovedEssayService
    {
        private readonly IStorageBroker storageBroker;

        public ImprovedEssayService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<ImprovedEssay> AddImprovedEssayAsync(ImprovedEssay improvedEssay) =>
            await this.storageBroker.InsertImprovedEssayAsync(improvedEssay);

        public IQueryable<ImprovedEssay> RetrieveAllImprovedEssays() =>
            this.storageBroker.RetrieveAllImprovedEssays();

        public async ValueTask<ImprovedEssay> ModifyEssayAsync(ImprovedEssay improvedEssay) =>
            throw new NotImplementedException();
    }
}
