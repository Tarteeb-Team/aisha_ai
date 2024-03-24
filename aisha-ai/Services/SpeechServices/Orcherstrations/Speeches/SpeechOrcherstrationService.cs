using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.ImprovedSpeechCheckers;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
using aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.SpeechServices.Foundations.Events.SpeechFeecbackEvents;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers;
using aisha_ai.Services.SpeechServices.Foundations.PronunciationAssessments;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbackCheckers;
using Microsoft.AspNetCore.Hosting;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.Speeches
{
    public class SpeechOrcherstrationService : ISpeechOrcherstrationService
    {
        private readonly IBlobService blobService;
        private readonly IPronunciationAssessmentService pronunciationAssessmentService;
        private readonly ISpeechFeedbackEventService speechFeedbackEventService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImprovedSpeechCheckerService improvedSpeechCheckerService;
        private readonly ISpeechFeedbackCheckerService speechFeedbackCheckerService;
        private readonly ITelegramService telegramService;


        public SpeechOrcherstrationService(
            IBlobService blobService,
            IPronunciationAssessmentService pronunciationAssessmentService,
            ISpeechFeedbackEventService speechFeedbackEventService,
            IWebHostEnvironment webHostEnvironment,
            IImprovedSpeechCheckerService improvedSpeechCheckerService,
            ISpeechFeedbackCheckerService speechFeedbackCheckerService,
            ITelegramService telegramService)
        {
            this.blobService = blobService;
            this.pronunciationAssessmentService = pronunciationAssessmentService;
            this.speechFeedbackEventService = speechFeedbackEventService;
            this.webHostEnvironment = webHostEnvironment;
            this.improvedSpeechCheckerService = improvedSpeechCheckerService;
            this.speechFeedbackCheckerService = speechFeedbackCheckerService;
            this.telegramService = telegramService;
        }

        public async ValueTask SpeechProcessAsync(string blobName)
        {
            try
            {
                using Stream speechStream = await this.blobService.DownloadSpeechAsync(blobName);
                string filePath = await SaveSpeechToLocalFolder(blobName, speechStream);
                string telegramUserName = TakeTelegramUserName(blobName);
                await PopulateAndAddImprovedSpeechCheckerAsync(telegramUserName);
                await PopulateAndAddSpeechFeedbackCheckerAsync(telegramUserName);

                SpeechFeedback speechFeedback = await this
                    .pronunciationAssessmentService.GetSpeechFeedbackAsync(filePath, telegramUserName);

                File.Delete(filePath);
                await this.speechFeedbackEventService.PublishSpeechFeedbackAsync(speechFeedback);
            }
            catch (Exception ex)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: 1924521160,
                    message: $"Error: {ex.Message}");
            }
        }

        private string TakeTelegramUserName(string blobName)
        {
            string[] parts = blobName.Split('.');
            string telegramUserName = parts[0];

            return telegramUserName;
        }

        private async Task<string> SaveSpeechToLocalFolder(string blobName, Stream speechStream)
        {
            var wwwrootPath = webHostEnvironment.WebRootPath;
            var filePath = Path.Combine(wwwrootPath, blobName);

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await speechStream.CopyToAsync(fileStream);
            }

            return filePath;
        }


        private async Task PopulateAndAddImprovedSpeechCheckerAsync(string telegramUserName)
        {
            var maybeImprovedSpeechChecker = this.improvedSpeechCheckerService
                .RetrieveAllImprovedSpeechCheckers().FirstOrDefault(i => i.TelegramUserName == telegramUserName);

            if (maybeImprovedSpeechChecker is null)
            {
                var improvedSpeechChecker = new ImprovedSpeechChecker()
                {
                    Id = Guid.NewGuid(),
                    State = false,
                    TelegramUserName = telegramUserName,
                };

                await this.improvedSpeechCheckerService.AddImprovedSpeechCheckerAsync(improvedSpeechChecker);
            }
        }

        private async Task PopulateAndAddSpeechFeedbackCheckerAsync(string telegramUserName)
        {
            var maybeSpeechfeedbackChecker = this.speechFeedbackCheckerService
                .RetrieveAllSpeechFeedbackCheckers().FirstOrDefault(s => s.TelegramUserName == telegramUserName);

            if (maybeSpeechfeedbackChecker is null)
            {
                var speechfeedbackChecker = new SpeechFeedbackChecker
                {
                    Id = Guid.NewGuid(),
                    State = false,
                    TelegramUserName = telegramUserName
                };

                await this.speechFeedbackCheckerService
                    .AddSpeechFeedbackCheckerAsync(speechfeedbackChecker);
            }
        }
    }
}
