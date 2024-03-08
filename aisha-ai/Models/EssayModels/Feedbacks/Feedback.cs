using System;

namespace aisha_ai.Models.EssayModels.Feedbacks;

public class Feedback
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string TelegramUserName { get; set; }
    public Guid TelegramUserId { get; set; }
}