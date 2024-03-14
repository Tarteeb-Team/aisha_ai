﻿using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Brokers.OpenAIs;
using aisha_ai.Services.Foundations.ImproveEssays;
using Standard.AI.OpenAI.Models.Services.Foundations.ChatCompletions;

namespace aisha_ai.Services.EssayServices.Foundations.ImproveEssays
{
    public class OpenAIService : IOpenAIService
    {
        private readonly IOpenAIBroker openAiBroker;

        public OpenAIService(IOpenAIBroker openAiBroker) =>
            this.openAiBroker = openAiBroker;

        public async ValueTask<string> AnalizeRequestAsync(string text, string message)
        {
            ChatCompletion request = CreateRequest(text, message);
            ChatCompletion result = await openAiBroker.AnalyzeEssayAsync(request);

            return result.Response.Choices.FirstOrDefault().Message.Content;
        }

        private static ChatCompletion CreateRequest(string text, string message)
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
                            Content = message,

                            Role = "system",
                        },
                        new ChatCompletionMessage
                        {
                            Content = text,
                            Role = "user",
                        }
                   },
                }
            };
        }
    }
}
