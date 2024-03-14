using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
using aisha_ai.Models.SpeechModels.Transcriptions;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.SpeechServices.Foundations.Events.SpeechFeecbackEvents;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbackCheckers;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbacks;

namespace aisha_ai.Services.SpeechServices.Orcherstrations.SpeechFeedbacks
{
    public class SpeechFeedbackOrcherstrationService : ISpeechFeedbackOrcherstrationService
    {
        private readonly ISpeechFeedbackService speechFeedbackService;
        private readonly ISpeechFeedbackCheckerService speechFeedbackCheckerService;
        private readonly ISpeechFeedbackEventService speechFeedbackEventService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;

        public SpeechFeedbackOrcherstrationService(
            ISpeechFeedbackService speechFeedbackService,
            ISpeechFeedbackCheckerService speechFeedbackCheckerService,
            ISpeechFeedbackEventService speechFeedbackEventService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService)
        {
            this.speechFeedbackService = speechFeedbackService;
            this.speechFeedbackCheckerService = speechFeedbackCheckerService;
            this.speechFeedbackEventService = speechFeedbackEventService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
        }

        public void ListenToSpeechFeedback(Func<Transcription, ValueTask> speechFeedbackHandler)
        {
            this.speechFeedbackEventService.ListenToSpeechFeedback(async (speechFeedback) =>
            {
                await ProcessSpeechFeedbackAsync(speechFeedback, speechFeedbackHandler);
            });
        }

        private async Task ProcessSpeechFeedbackAsync(
            SpeechFeedback speechFeedback,
            Func<Transcription, ValueTask> speechFeedbackHandler)
        {
            try
            {
                await EnsureSpeechFeedbackAsync(speechFeedback);
                await ModifySpeechFeedbackCheckerAsync(speechFeedback);
                var transcription = CreateTranscriptionFromSpeechFeedback(speechFeedback);
                await NotifyAdminAsync(speechFeedback);
                await speechFeedbackHandler(transcription);
            }
            catch (Exception ex)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: 1924521160,
                    message: $"Error: {ex.Message}");
            }
        }

        private async Task NotifyAdminAsync(SpeechFeedback speechFeedback)
        {
            var telegramUser = this.telegramUserService.RetrieveAllTelegramUsers()
                .FirstOrDefault(t => t.TelegramUserName == speechFeedback.TelegramUserName);

            await this.telegramService.SendMessageAsync(
                userTelegramId: telegramUser.TelegramId,
                message: $"Speech:\nSpeech feedback is done.\nUser: {telegramUser.TelegramUserName}");
        }

        private Transcription CreateTranscriptionFromSpeechFeedback(SpeechFeedback speechFeedback)
        {
            return new Transcription
            {
                Id = Guid.NewGuid(),
                Content = speechFeedback.Transcription,
                TelegramUserName = speechFeedback.TelegramUserName
            };
        }

        private async Task ModifySpeechFeedbackCheckerAsync(SpeechFeedback speechFeedback)
        {
            var speechfeedbackChecker = this.speechFeedbackCheckerService.RetrieveAllSpeechFeedbackCheckers()
                .FirstOrDefault(s => s.TelegramUserName == speechFeedback.TelegramUserName);

            speechfeedbackChecker.State = true;

            await this.speechFeedbackCheckerService
                .ModifySpeechFeedbackCheckerAsync(speechfeedbackChecker);
        }

        private async Task EnsureSpeechFeedbackAsync(SpeechFeedback speechFeedback)
        {
            var maybeSpeechFeedback = this.speechFeedbackService.RetrieveAllSpeechFeedbacks()
                .FirstOrDefault(m => m.TelegramUserName == speechFeedback.TelegramUserName);

            if (maybeSpeechFeedback is null)
                await this.speechFeedbackService.AddSpeechFeedbackAsync(speechFeedback);
            else
            {
                maybeSpeechFeedback.Transcription = speechFeedback.Transcription;
                maybeSpeechFeedback.AccuracyScore = speechFeedback.AccuracyScore;
                maybeSpeechFeedback.FluencyScore = speechFeedback.FluencyScore;
                maybeSpeechFeedback.ProsodyScore = speechFeedback.ProsodyScore;
                maybeSpeechFeedback.CompletenessScore = speechFeedback.CompletenessScore;
                maybeSpeechFeedback.PronunciationScore = speechFeedback.PronunciationScore;

                await this.speechFeedbackService.ModifySpeechFeedbackAsync(maybeSpeechFeedback);
            }
        }
    }
}
