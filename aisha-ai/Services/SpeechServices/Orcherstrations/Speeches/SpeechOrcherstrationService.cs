using System.IO;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
using aisha_ai.Services.Foundations.Bloobs;
using aisha_ai.Services.SpeechServices.Foudations.Events.SpeechFeecbackEvents;
using aisha_ai.Services.SpeechServices.Foudations.PronunciationAssessments;
using Microsoft.AspNetCore.Hosting;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.Speeches
{
    public class SpeechOrcherstrationService : ISpeechOrcherstrationService
    {
        private readonly IBlobService blobService;
        private readonly IPronunciationAssessmentService pronunciationAssessmentService;
        private readonly ISpeechFeedbackEventService speechFeedbackEventService;
        private readonly IWebHostEnvironment webHostEnvironment;


        public SpeechOrcherstrationService(
            IBlobService blobService,
            IPronunciationAssessmentService pronunciationAssessmentService,
            ISpeechFeedbackEventService speechFeedbackEventService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.blobService = blobService;
            this.pronunciationAssessmentService = pronunciationAssessmentService;
            this.speechFeedbackEventService = speechFeedbackEventService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async ValueTask SpeechProcessAsync(string blobName)
        {
            using Stream speechStream = await this.blobService.DownloadSpeechAsync(blobName);
            string filePath = await SaveSpeechToLocalFolder(blobName, speechStream);
            string telegramUserName = TakeTelegramUserName(blobName);

            Models.SpeechModels.SpeechesFeedback.SpeechFeedback speechFeedback = await this
                .pronunciationAssessmentService.GetSpeechFeedbackAsync(filePath, telegramUserName);

            await this.speechFeedbackEventService.PublishSpeechFeedbackAsync(speechFeedback);
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
            var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await speechStream.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
