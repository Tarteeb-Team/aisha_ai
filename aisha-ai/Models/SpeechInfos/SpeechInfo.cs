using System;

namespace aisha_ai.Models.SpeechInfos;

public class SpeechInfo
{
    public Guid Id { get; set; }
    public string BlobName { get; set; }
    public string TelegramUserName { get; set; }
}