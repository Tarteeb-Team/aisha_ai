using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Essays;
using aisha_ai.Models.Feedbacks;
using aisha_ai.Services.Foundations.Checkers;
using aisha_ai.Services.Foundations.EssayAnalizers;
using aisha_ai.Services.Foundations.FeedbackEvents;
using aisha_ai.Services.Foundations.Feedbacks;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;

namespace aisha_ai.Services.Orchestrations.Feedbacks
{
    public class FeedbackOrchestrationService : IFeedbackOrchestrationService
    {
        private readonly IEssayAnalyzerService essayAnalyzerService;
        private readonly IFeedbackService feedbackService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;
        private readonly ICheckerService checkerService;
        private readonly IFeedbackEventService feedbackEventService;

        public FeedbackOrchestrationService(
            IEssayAnalyzerService essayAnalyzerService,
            IFeedbackService feedbackService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService,
            ICheckerService checkerService,
            IFeedbackEventService feedbackEventService)
        {
            this.essayAnalyzerService = essayAnalyzerService;
            this.feedbackService = feedbackService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
            this.checkerService = checkerService;
            this.feedbackEventService = feedbackEventService;
        }

        public async ValueTask ProcessFeedbackAsync(Essay essay)
        {
            var feedback = await EnsureFeedbackAsync(essay);
            await this.feedbackEventService.PublishFeedbackAsync(feedback);

            var telegramUser = this.telegramUserService
                .RetrieveAllTelegramUsers().FirstOrDefault(t => t.TelegramUserName == essay.TelegramUserName);

            await ModifyCheckerAsync(essay);

            await this.telegramService.SendMessageAsync(
                userTelegramId: telegramUser.TelegramId,
                message: "Feedback is done");
        }

        private async ValueTask<Feedback> EnsureFeedbackAsync(Essay essay)
        {
            var content = await this.essayAnalyzerService.AnalyzeEssayAsync(essay.Content);

            var maybeFeedback = this.feedbackService.RetrieveAllFeedbacks()
                .FirstOrDefault(f => f.TelegramUserName == essay.TelegramUserName);

            if (maybeFeedback is not null)
            {
                return await ModifyFeedback(content, maybeFeedback);
            }
            else
            {
                var feedback = PopulateFeedback(content, essay);
                return await this.feedbackService.AddFeedbackAsync(feedback);
            }
        }

        private async ValueTask ModifyCheckerAsync(Essay essay)
        {
            var checker = this.checkerService.RetrieveAllCheckers()
                .FirstOrDefault(c => c.TelegramUserName == essay.TelegramUserName);

            checker.State = true;

            await this.checkerService.ModifyCheckerAsync(checker);
        }

        private Feedback PopulateFeedback(string content, Essay essay)
        {
            return new Feedback
            {
                Id = Guid.NewGuid(),
                Content = content,
                TelegramUserName = essay.TelegramUserName,
                TelegramUserId = essay.TelegramUserId
            };
        }

        private async ValueTask<Feedback> ModifyFeedback(string content, Feedback feedback)
        {
            feedback.Content = content;

            return await this.feedbackService.ModifyFeedbackAsync(feedback);
        }
    }
}
