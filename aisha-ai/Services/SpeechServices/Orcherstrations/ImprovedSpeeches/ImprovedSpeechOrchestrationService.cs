using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers;
using aisha_ai.Models.SpeechModels.Transcriptions;
using aisha_ai.Services.EssayServices.Foundations.Speeches;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.ImprovedSpeeches
{
    public class ImprovedSpeechOrchestrationService : IImprovedSpeechOrchestrationService
    {
        private readonly IOpenAIService openAIService;
        private readonly ISpeechService speechService;
        private readonly IBlobService blobService;
        private readonly ITelegramService telegramService;
        private readonly IImprovedSpeechCheckerService improvedSpeechChecker;

        public ImprovedSpeechOrchestrationService(
            IOpenAIService openAIService,
            ISpeechService speechService,
            IBlobService blobService,
            ITelegramService telegramService,
            IImprovedSpeechCheckerService improvedSpeechChecker)
        {
            this.openAIService = openAIService;
            this.speechService = speechService;
            this.blobService = blobService;
            this.telegramService = telegramService;
            this.improvedSpeechChecker = improvedSpeechChecker;
        }

        public async ValueTask ProcessImproveSpeechAsync(Transcription transcription)
        {
            string messageForAI = "This is my speech, you should improve it by 1-2 points according to ielts score.";
            var content = await this.openAIService.AnalizeRequestAsync(transcription.Content, messageForAI);
            var fileName = $"{transcription.TelegramUserName}.IS";
            var filePath = await this.speechService.CreateAndSaveSpeechAudioAsync(content, fileName);
            await EnsureBlobAsync(fileName, filePath);
            await PopulateAndAddSpeechFeedbackCheckerAsync(transcription);
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

        private async Task PopulateAndAddSpeechFeedbackCheckerAsync(Transcription transcription)
        {
            var improvedSpeechChecker = new ImprovedSpeechChecker()
            {
                Id = Guid.NewGuid(),
                State = true,
                TelegramUserName = transcription.TelegramUserName,
            };

            await this.improvedSpeechChecker.AddImprovedSpeechCheckerAsync(improvedSpeechChecker);
        }
    }
}
