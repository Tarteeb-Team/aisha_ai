using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeeches;
using aisha_ai.Models.SpeechModels.Transcriptions;
using aisha_ai.Services.EssayServices.Foundations.Speeches;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeeches;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.ImprovedSpeeches
{
    public class ImprovedSpeechOrchestrationService : IImprovedSpeechOrchestrationService
    {
        private readonly IOpenAIService openAIService;
        private readonly ISpeechService speechService;
        private readonly IBlobService blobService;
        private readonly ITelegramService telegramService;
        private readonly IImprovedSpeechCheckerService improvedSpeechCheckerService;
        private readonly IImprovedSpeechService improvedSpeechService;
        private readonly ITelegramUserService telegramUserService;

        public ImprovedSpeechOrchestrationService(
            IOpenAIService openAIService,
            ISpeechService speechService,
            IBlobService blobService,
            ITelegramService telegramService,
            IImprovedSpeechCheckerService improvedSpeechCheckerService,
            IImprovedSpeechService improvedSpeechService,
            ITelegramUserService telegramUserService)
        {
            this.openAIService = openAIService;
            this.speechService = speechService;
            this.blobService = blobService;
            this.telegramService = telegramService;
            this.improvedSpeechCheckerService = improvedSpeechCheckerService;
            this.improvedSpeechService = improvedSpeechService;
            this.telegramUserService = telegramUserService;
        }

        public async ValueTask ProcessImproveSpeechAsync(Transcription transcription)
        {
            try
            {
                string messageForAI = "This is my speech, you should improve it by 1-2 points according to ielts score. Give only improved text.";
                var content = await this.openAIService.AnalizeRequestAsync(transcription.Content, messageForAI);
                var fileName = $"{transcription.TelegramUserName}.IS";
                var filePath = await this.speechService.CreateAndSaveSpeechAudioAsync(content, fileName);
                await EnsureBlobAsync($"{fileName}.wav", filePath);
                await EnsureImprovedSpeechAsync(transcription.TelegramUserName, content);
                await ModifyImprovedSpeechCheckerAsync(transcription);
                await NotifyAdminAsync(transcription);
            }
            catch (Exception ex)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: 1924521160,
                    message: $"Error: {ex.Message}");
            }

        }

        private async Task NotifyAdminAsync(Transcription transcription)
        {
            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                            .FirstOrDefault(t => t.TelegramUserName == transcription.TelegramUserName);

            await this.telegramService.SendMessageAsync(
                userTelegramId: telegramUser.TelegramId,
                message: $"Speech:\nImproved Speech is done.\nUser: {telegramUser.TelegramUserName}");
        }

        private async Task EnsureImprovedSpeechAsync(string telegramUseraName, string content)
        {
            var maybeImprovedSpeech = this.improvedSpeechService.RetrieveAllImprovedSpeechs()
                .FirstOrDefault(i => i.TelegramUserName == telegramUseraName);

            if (maybeImprovedSpeech is null)
            {
                var improvedSpeech = new ImprovedSpeech
                {
                    Id = Guid.NewGuid(),
                    Content = content,
                    TelegramUserName = telegramUseraName
                };

                await this.improvedSpeechService.AddImprovedSpeechAsync(improvedSpeech);
            }
            else
            {
                maybeImprovedSpeech.Content = content;
                await this.improvedSpeechService.ModifyImprovedSpeechAsync(maybeImprovedSpeech);
            }
        }

        private async Task EnsureBlobAsync(string fileName, string filePath)
        {
            try
            {
                var fileStream = System.IO.File.OpenRead(filePath);
                bool exists = await this.blobService.CheckIfBlobExistsAsync(fileName);

                if (exists)
                    await this.blobService.RemoveSpeechAsync(fileName);

                await this.blobService.UploadSpeechAsync(fileStream, fileName);

                await this.telegramService
                    .SendMessageAsync(1924521160, $"Save to the blob is done (speech)");
            }
            catch (Exception ex)
            {
                await this.telegramService
                    .SendMessageAsync(1924521160, $"Error at ensure blob: {ex.Message}");

                throw ex;
            }
        }

        private async Task ModifyImprovedSpeechCheckerAsync(Transcription transcription)
        {
            var improvedSpeechChecker = this.improvedSpeechCheckerService.RetrieveAllImprovedSpeechCheckers()
                .FirstOrDefault(i => i.TelegramUserName == transcription.TelegramUserName);

            improvedSpeechChecker.State = true;

            await this.improvedSpeechCheckerService
                .ModifyImprovedSpeechCheckerAsync(improvedSpeechChecker);
        }
    }
}
