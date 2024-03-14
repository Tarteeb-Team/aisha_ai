using System;

namespace aisha_ai.Models.SpeechModels.Transcriptions
{
    public class Transcription
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string TelegramUserName { get; set; }
    }
}
