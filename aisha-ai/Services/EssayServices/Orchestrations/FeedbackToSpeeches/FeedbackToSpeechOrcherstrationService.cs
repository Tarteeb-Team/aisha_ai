﻿using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.EssayModels.SpeechInfos;
using aisha_ai.Services.EssayServices.Foundations.Events.FeedbackEvents;
using aisha_ai.Services.EssayServices.Foundations.Speeches;
using aisha_ai.Services.EssayServices.Orchestrations.FeedbackToSpeeches;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.SpeechInfos;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;

namespace aisha_ai.Services.Orchestrations.FeedbackToSpeeches
{
    public class FeedbackToSpeechOrcherstrationService : IFeedbackToSpeechOrcherstrationService
    {
        private readonly ISpeechService speechService;
        private readonly IBlobService blobService;
        private readonly ISpeechInfoService speechInfoService;
        private readonly IFeedbackEventService feedbackEventService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;

        public FeedbackToSpeechOrcherstrationService(
            ISpeechService speechService,
            IBlobService blobService,
            ISpeechInfoService speechInfoService,
            IFeedbackEventService feedbackEventService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService)
        {
            this.speechService = speechService;
            this.blobService = blobService;
            this.speechInfoService = speechInfoService;
            this.feedbackEventService = feedbackEventService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
        }

        public void ListenToFeedback() =>
            this.feedbackEventService.ListenToFeedback(ProcessSpeechAsync);

        private async ValueTask ProcessSpeechAsync(Feedback feedback)
        {
            try
            {
                var fileName = $"{feedback.TelegramUserName}.wav";

                var telegramUser = this.telegramUserService
                    .RetrieveAllTelegramUsers().FirstOrDefault(
                        t => t.TelegramUserName == feedback.TelegramUserName);

                string filePath = await this.speechService
                    .CreateAndSaveSpeechAudioAsync(feedback.Content, feedback.TelegramUserName);

                using FileStream fileStream = await EnsureBlobAsync(fileName, filePath);

                await this.telegramService.SendMessageAsync(
                    telegramUser.TelegramId, $"Save to blob is done\nUser: {feedback.TelegramUserName}");

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
