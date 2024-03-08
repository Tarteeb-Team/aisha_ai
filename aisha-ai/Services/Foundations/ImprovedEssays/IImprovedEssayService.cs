using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.ImprovedEssays;

namespace aisha_ai.Services.Foundations.ImprovedEssays
{
    public interface IImprovedEssayService
    {
        public ValueTask<ImprovedEssay> AddImprovedEssayAsync(ImprovedEssay improvedEssay);
        public IQueryable<ImprovedEssay> RetrieveAllImprovedEssays();
        public ValueTask<ImprovedEssay> ModifyEssayAsync(ImprovedEssay improvedEssay);
        public ValueTask<ImprovedEssay> RemoveImprovedEssayAsync(ImprovedEssay improvedEssay);
    }
}