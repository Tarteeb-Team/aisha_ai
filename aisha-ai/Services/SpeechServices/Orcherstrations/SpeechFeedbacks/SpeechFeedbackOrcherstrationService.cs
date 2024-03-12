using System;
using System.Threading.Tasks;
using aisha_ai.Models.SpeechModels.SpeechesFeedback;
using aisha_ai.Services.SpeechServices.Foudations.Events.SpeechFeecbackEvents;
using aisha_ai.Services.SpeechServices.Foudations.SpeechFeedbackCheckers;
using aisha_ai.Services.SpeechServices.Foudations.SpeechFeedbacks;

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

        public void ListenToSpeechFeedback(Func<SpeechFeedback, ValueTask> speechFeedbackHandler)
        {
            this.speechFeedbackEventService.ListenToSpeechFeedback(async (speechFeedback) =>
            {
                await ProcessSpeechFeedbackAsync(speechFeedback, speechFeedbackHandler);
            });
        }

        private async Task ProcessSpeechFeedbackAsync(
            SpeechFeedback speechFeedback,
            Func<SpeechFeedback, ValueTask> speechFeedbackHandler)
        {
            // ... 
        }
    }
}
