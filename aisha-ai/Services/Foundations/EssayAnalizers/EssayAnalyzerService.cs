using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace aisha_ai.Services.Foundations.EssayAnalizers
{
    public class EssayAnalyzerService : IEssayAnalyzerService
    {
        private readonly IOpenAIBroker openAiBroker;

        public EssayAnalyzerService(IOpenAIBroker openAiBroker) =>
            this.openAiBroker = openAiBroker;

        public async ValueTask<string> AnalyzeEssayAsync(string essay)
        {
            ChatCompletion request = CreateRequest(essay);
            ChatCompletion result = await this.openAiBroker.AnalyzeEssayAsync(request);

            return result.Response.Choices.FirstOrDefault().Message.Content;
        }

        private static ChatCompletion CreateRequest(string essay)
        {
            return new ChatCompletion
            {
                Request = new ChatCompletionRequest
                {
                    Model = "gpt-4-1106-preview",
                    MaxTokens = 1500,
                    Messages = new ChatCompletionMessage[]
                   {
                       new ChatCompletionMessage
                        {
                            Content = "You are IELTS Writing examiner. Give detailed IELTS feedback" +
                                "based on marking criteria of IELTS and give me overall Band.",

                            Role = "system",
                        },
                        new ChatCompletionMessage
                        {
                            Content = essay,
                            Role = "user",
                        }
                   },
                }
            };
        }
    }
}
