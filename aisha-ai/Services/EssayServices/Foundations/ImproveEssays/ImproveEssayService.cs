using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.OpenAIs;
using aisha_ai.Services.Foundations.ImproveEssays;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace aisha_ai.Services.EssayServices.Foundations.ImproveEssays
{
    public class ImproveEssayService : IImproveEssayService
    {
        private readonly IOpenAIBroker openAiBroker;

        public ImproveEssayService(IOpenAIBroker openAiBroker) =>
            this.openAiBroker = openAiBroker;

        public async ValueTask<string> ImproveEssayAsync(string essay)
        {
            ChatCompletion request = CreateRequest(essay);
            ChatCompletion result = await openAiBroker.AnalyzeEssayAsync(request);

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
                            Content = "Improve my essay by 1-2 points according to ielts score.",

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
