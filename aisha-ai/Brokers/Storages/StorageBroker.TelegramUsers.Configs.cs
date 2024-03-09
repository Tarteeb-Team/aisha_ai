using aisha_ai.Models.EssayModels.FeedbackCheckers;
using aisha_ai.Models.EssayModels.TelegramUsers;
using aisha_ai.Models.EssayModels.UploadPhotoChekers;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddTelegramUserConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramUser>()
                .HasOne(telegramUser => telegramUser.Checker)
                .WithOne(photoChecker => photoChecker.TelegramUser)
                .HasForeignKey<PhotoChecker>(photoChecker => photoChecker.TelegramUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TelegramUser>()
                .HasOne(telegramUser => telegramUser.FeedbackChecker)
                .WithOne(feedbackChecker => feedbackChecker.TelegramUser)
                .HasForeignKey<FeedbackChecker>(feedbackChecker => feedbackChecker.TelegramUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
