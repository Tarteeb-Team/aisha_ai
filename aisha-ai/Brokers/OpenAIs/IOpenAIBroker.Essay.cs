using System.Threading.Tasks;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace aisha_ai.Brokers.OpenAIs
{
    public partial interface IOpenAIBroker
    {
        ValueTask<ChatCompletion> AnalyzeEssayAsync(ChatCompletion chatCompletion);
    }
}
