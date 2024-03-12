using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.SpeechConfigurations;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.PronunciationAssessment;
using Microsoft.Extensions.Configuration;

namespace aisha_ai.Brokers.Cognitives
{
    public class PronunciationAssessmentBroker : IPronunciationAssessmentBroker
    {
        private readonly SpeechConfig speechConfig;

        public PronunciationAssessmentBroker(
            IConfiguration configuration)
        {
            var speechConfiguration = new SpeechConfiguration();
            configuration.Bind(key: "SpeechConfiguration", instance: speechConfiguration);

            this.speechConfig = SpeechConfig.FromSubscription(
                subscriptionKey: speechConfiguration.SubscriptionKey,
                region: speechConfiguration.Region);

            this.speechConfig.SpeechRecognitionLanguage = "en-US";
        }

        public async ValueTask<string> GetSpeechFeedbackJsonStringAsync(string filePath)
        {
            var pronunciationAssessmentConfig = new PronunciationAssessmentConfig(
                referenceText: "",
                gradingSystem: GradingSystem.HundredMark,
                granularity: Granularity.Phoneme,
                enableMiscue: false);
            pronunciationAssessmentConfig.EnableProsodyAssessment();
            pronunciationAssessmentConfig.EnableContentAssessmentWithTopic("greeting");

            using var audioConfig = AudioConfig.FromWavFileInput(filePath);

            using (var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig))
            {
                pronunciationAssessmentConfig.ApplyTo(speechRecognizer);
                var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();

                var pronunciationAssessmentResult =
                    PronunciationAssessmentResult.FromResult(speechRecognitionResult);

                var pronunciationAssessmentResultJson = speechRecognitionResult
                    .Properties.GetProperty(PropertyId.SpeechServiceResponse_JsonResult);

                return pronunciationAssessmentResultJson;
            }
        }
    }
}
