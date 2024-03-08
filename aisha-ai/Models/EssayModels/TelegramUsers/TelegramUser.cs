using System;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;
using aisha_ai.Models.EssayModels.FeedbackCheckers;

namespace aisha_ai.Models.EssayModels.TelegramUsers
{
    public class TelegramUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long TelegramId { get; set; }
        public string TelegramUserName { get; set; }
        public TelegramUserStatus TelegramUserStatus { get; set; }
        public Guid CheckerId { get; set; }
        public Guid FeedbackCheckerId { get; set; }

        public PhotoChecker Checker { get; set; }
        public FeedbackChecker FeedbackChecker { get; set; }
    }
}
