using System;
using System.IO;
using System.Threading.Tasks;
using aisha_ai.Models.Feedbacks;
using aisha_ai.Models.SpeechInfos;
using aisha_ai.Models.TelegramUsers;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.FeedbackEvents;
using aisha_ai.Services.Foundations.Speeches;
using aisha_ai.Services.Foundations.SpeechInfos;
using aisha_ai.Services.Foundations.Telegrams;
using StackExchange.Redis;

namespace aisha_ai.Services.Orchestrations.FeedbackToSpeeches
{
    public class FeedbackToSpeechOrcherstrationService : IFeedbackToSpeechOrcherstrationService
    {
        private readonly ISpeechService speechService;
        private readonly IBlobService blobService;
        private readonly ISpeechInfoService speechInfoService;
        private readonly IFeedbackEventService feedbackEventService;
        private readonly ITelegramService telegramService;

        public FeedbackToSpeechOrcherstrationService(
            ISpeechService speechService,
            IBlobService blobService,
            ISpeechInfoService speechInfoService,
            IFeedbackEventService feedbackEventService,
            ITelegramService telegramService)
        {
            this.speechService = speechService;
            this.blobService = blobService;
            this.speechInfoService = speechInfoService;
            this.feedbackEventService = feedbackEventService;
            this.telegramService = telegramService;
        }

        public void ListenToFeedback() =>
            this.feedbackEventService.ListenToFeedback(ProcessSpeechAsync);

        private async ValueTask ProcessSpeechAsync(Feedback feedback)
        {
            try
            {
                var fileName = $"{feedback.TelegramUserName}.wav";

                string filePath = await this.speechService
                    .SaveSpeechAudioAsync(feedback.Content, feedback.TelegramUserName);

                using FileStream fileStream = await EnsureBlobAsync(fileName, filePath);

                await this.telegramService.SendMessageAsync(
                    1924521160, $"Save to blob is done\nUser: {feedback.TelegramUserName}");

                await PopulateAndAddSpeechInfoAsync(feedback, fileName);
            }
            catch (Exception ex)
            {
                await this.telegramService.SendMessageAsync(
                    1924521160, $"Error at process speech: {ex.Message}\nUser: {feedback.TelegramUserName}");
            }
        }

        private async Task<FileStream> EnsureBlobAsync(string fileName, string filePath)
        {
            try
            {
                var fileStream = File.OpenRead(filePath);
                bool exists = await this.blobService.CheckIfBlobExistsAsync(fileName);

                if (exists)
                {
                    await this.blobService.RemoveSpeechAsync(fileName);
                }

                await this.blobService.UploadSpeechAsync(fileStream, fileName);

                return fileStream;
            }
            catch (Exception ex)
            {
                await this.telegramService
                    .SendMessageAsync(1924521160, $"Error at ensure blob: {ex.Message}");

                throw ex;
            }
        }

        private async Task PopulateAndAddSpeechInfoAsync(Feedback feedback, string fileName)
        {
            var speechInfo = new SpeechInfo
            {
                Id = Guid.NewGuid(),
                BlobName = fileName,
                TelegramUserName = feedback.TelegramUserName
            };

            await this.speechInfoService.AddSpeechInfoAsync(speechInfo);
        }
    }
}
