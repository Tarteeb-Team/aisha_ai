using System.Threading.Tasks;

namespace aisha_ai.Services.Foundations.ImproveEssays
{
    public interface IOpenAIService
    {
        ValueTask<string> AnalizeRequestAsync(string text, string message);
    }
}
