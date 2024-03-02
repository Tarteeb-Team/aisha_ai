using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

namespace aisha_ai.Brokers.Speeches
{
    public interface ISpeechBroker
    {
        ValueTask<SpeechSynthesisResult> GetSpeechResultAsync(string text);
    }
}
