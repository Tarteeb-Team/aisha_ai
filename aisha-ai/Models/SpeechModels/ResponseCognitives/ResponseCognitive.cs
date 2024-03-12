using System.Collections.Generic;
using aisha_ai.Models.SpeechModels.NBests;

namespace aisha_ai.Models.SpeechModels.ResponseCognitives
{
    public class ResponseCognitive
    {
        public string Id { get; set; }
        public string RecognitionStatus { get; set; }
        public string DisplayText { get; set; }
        public List<NBest> NBest { get; set; }
    }
}
