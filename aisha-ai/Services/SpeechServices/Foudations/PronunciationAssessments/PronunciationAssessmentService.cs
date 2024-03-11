using System;
using System.Threading.Tasks;
using aisha_ai.Brokers.Cognitives;
using aisha_ai.Models.SpeechModels.ResponseCognitives;
using aisha_ai.Models.SpeechModels.SpeechFeedback;
using Newtonsoft.Json;

namespace aisha_ai.Services.SpeechServices.Foudations.PronunciationAssessments
{
    public class PronunciationAssessmentService : IPronunciationAssessmentService
    {
        private readonly IPronunciationAssessmentBroker pronunciationAssessmentBroker;

        public PronunciationAssessmentService(IPronunciationAssessmentBroker pronunciationAssessmentBroker) =>
            this.pronunciationAssessmentBroker = pronunciationAssessmentBroker;

        public async ValueTask<SpeechFeedback> GetSpeechFeedbackAsync(string filePath, string telegramUserName)
        {
            string speechFeedbackJsonResult = await this
                .pronunciationAssessmentBroker.GetSpeechFeedbackJsonStringAsync(filePath);

            return PopulateSpeechFeedback(speechFeedbackJsonResult, telegramUserName);
        }

        private SpeechFeedback PopulateSpeechFeedback(string speechFeedbackJsonResult, string telegramUserName)
        {
            ResponseCognitive responseCognitive = new ResponseCognitive();
            responseCognitive = JsonConvert.DeserializeObject<ResponseCognitive>(speechFeedbackJsonResult);

            var speechFeedback = new SpeechFeedback
            {
                Id = Guid.NewGuid(),
                Transcription = responseCognitive.DisplayText,
                AccurancyScore = responseCognitive.NBest[0].SpeechFeedback.AccurancyScore,
                FluencyScore = responseCognitive.NBest[0].SpeechFeedback.FluencyScore,
                ProsodyScore = responseCognitive.NBest[0].SpeechFeedback.ProsodyScore,
                CompletenessScore = responseCognitive.NBest[0].SpeechFeedback.CompletenessScore,
                PronunciationScore = responseCognitive.NBest[0].SpeechFeedback.PronunciationScore,
                TelegramUserName = telegramUserName
            };

            return speechFeedback;
        }
    }
}
