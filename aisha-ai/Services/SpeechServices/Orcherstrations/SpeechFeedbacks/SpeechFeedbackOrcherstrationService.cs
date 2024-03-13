using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
using aisha_ai.Models.SpeechModels.SpeechFeedbackCheckers;
using aisha_ai.Models.SpeechModels.Transcriptions;
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

        public SpeechFeedbackOrcherstrationService(
            ISpeechFeedbackService speechFeedbackService,
            ISpeechFeedbackCheckerService speechFeedbackCheckerService,
            ISpeechFeedbackEventService speechFeedbackEventService)
        {
            this.speechFeedbackService = speechFeedbackService;
            this.speechFeedbackCheckerService = speechFeedbackCheckerService;
            this.speechFeedbackEventService = speechFeedbackEventService;
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
            await this.speechFeedbackService.AddSpeechFeedbackAsync(speechFeedback);
            await PopulateAndAddSpeechFeedbackCheckerAsync(speechFeedback);
            var transcription = CreateTranscriptionFromSpeechFeedback(speechFeedback);

            await speechFeedbackHandler(transcription);
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

        private async Task PopulateAndAddSpeechFeedbackCheckerAsync(SpeechFeedback speechFeedback)
        {
            var speechfeedbackChecker = new SpeechFeedbackChecker
            {
                Id = Guid.NewGuid(),
                State = true,
                TelegramUserName = speechFeedback.TelegramUserName
            };

            await this.speechFeedbackCheckerService
                .AddSpeechFeedbackCheckerAsync(speechfeedbackChecker);
        }
    }
}
