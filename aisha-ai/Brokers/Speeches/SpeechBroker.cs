using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.SpeechConfigurations;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

namespace aisha_ai.Brokers.Speeches
{
    public class SpeechBroker : ISpeechBroker
    {
        private readonly SpeechConfig speechConfig;

        public SpeechBroker(
            IConfiguration configuration)
        {
            var speechConfiguration = new SpeechConfiguration();
            configuration.Bind(key: "SpeechConfiguration", instance: speechConfiguration);

            this.speechConfig = SpeechConfig.FromSubscription(
                subscriptionKey: speechConfiguration.SubscriptionKey,
                region: speechConfiguration.Region);

            this.speechConfig.SpeechSynthesisVoiceName = speechConfiguration.SpeechVoice;
        }

        public async ValueTask<SpeechSynthesisResult> GetSpeechResultAsync(string text)
        {
            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig, null))
            {
                SpeechSynthesisResult speechResult =
                    await speechSynthesizer.SpeakTextAsync(text: text);

                return speechResult;
            }
        }
    }
}
