using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.Transcriptions;
using aisha_ai.Services.EssayServices.Foundations.Speeches;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.Telegrams;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.ImprovedSpeeches
{
    public class ImprovedSpeechOrchestrationService : IImprovedSpeechOrchestrationService
    {
        private readonly IOpenAIService openAIService;
        private readonly ISpeechService speechService;
        private readonly IBlobService blobService;
        private readonly ITelegramService telegramService;

        public ImprovedSpeechOrchestrationService(
            IOpenAIService openAIService,
            ISpeechService speechService,
            IBlobService blobService,
            ITelegramService telegramService)
        {
            this.openAIService = openAIService;
            this.speechService = speechService;
            this.blobService = blobService;
            this.telegramService = telegramService;
        }

        public async ValueTask ProcessImproveSpeechAsync(Transcription transcription)
        {
            string messageForAI = "This is my speech, you should improve it by 1-2 points according to ielts score.";
            var content = await this.openAIService.AnalizeRequestAsync(transcription.Content, messageForAI);
            var fileName = $"{transcription.TelegramUserName}.IS";
            var filePath = await this.speechService.CreateAndSaveSpeechAudioAsync(content, fileName);
            await EnsureBlobAsync(fileName, filePath);
        }

        private async Task EnsureBlobAsync(string fileName, string filePath)
        {
            try
            {
                var fileStream = System.IO.File.OpenRead(filePath);
                bool exists = await this.blobService.CheckIfBlobExistsAsync(fileName);

                if (exists)
                {
                    await this.blobService.RemoveSpeechAsync(fileName);
                }

                await this.blobService.UploadSpeechAsync(fileStream, fileName);
            }
            catch (Exception ex)
            {
                await this.telegramService
                    .SendMessageAsync(1924521160, $"Error at ensure blob: {ex.Message}");

                throw ex;
            }
        }
    }
}
