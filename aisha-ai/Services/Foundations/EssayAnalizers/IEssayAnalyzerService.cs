using System.Threading.Tasks;

namespace aisha_ai.Services.Foundations.EssayAnalizers
{
    public interface IEssayAnalyzerService
    {
        ValueTask<string> AnalyzeEssayAsync(string essay);
    }
}
