using aisha_ai.Models.Chekers;
using aisha_ai.Models.FeedbackCheckers;
using aisha_ai.Models.TelegramUsers;
using Microsoft.EntityFrameworkCore;

namespace aisha_ai.Brokers.Storages
{
    public partial class StorageBroker
    {
        private static void AddTelegramUserConfigurations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TelegramUser>()
                .HasOne(telegramUser => telegramUser.Checker)
                .WithOne(checker => checker.TelegramUser)
                .HasForeignKey<Checker>(checker => checker.TelegramUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TelegramUser>()
                .HasOne(telegramUser => telegramUser.FeedbackChecker)
                .WithOne(feedbackChecker => feedbackChecker.TelegramUser)
                .HasForeignKey<FeedbackChecker>(feedbackChecker => feedbackChecker.TelegramUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
