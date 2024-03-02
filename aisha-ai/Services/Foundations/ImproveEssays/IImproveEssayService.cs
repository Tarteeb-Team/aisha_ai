using System.Threading.Tasks;

namespace aisha_ai.Services.Foundations.ImproveEssays
{
    public interface IImproveEssayService
    {
        ValueTask<string> ImproveEssayAsync(string essay);
    }
}
