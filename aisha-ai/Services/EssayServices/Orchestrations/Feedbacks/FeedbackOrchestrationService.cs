﻿using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;
using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Services.EssayServices.Foundations.Events.FeedbackEvents;
using aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers;
using aisha_ai.Services.Foundations.Feedbacks;
using aisha_ai.Services.Foundations.ImproveEssays;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;

namespace aisha_ai.Services.Orchestrations.Feedbacks
{
    public class FeedbackOrchestrationService : IFeedbackOrchestrationService
    {
        private readonly IFeedbackService feedbackService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;
        private readonly IFeedbackCheckerService feedbackCheckerService;
        private readonly IFeedbackEventService feedbackEventService;
        private readonly IOpenAIService openAIService;

        public FeedbackOrchestrationService(
            IFeedbackService feedbackService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService,
            IFeedbackEventService feedbackEventService,
            IFeedbackCheckerService feedbackCheckerService,
            IOpenAIService openAIService)
        {
            this.feedbackService = feedbackService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
            this.feedbackEventService = feedbackEventService;
            this.feedbackCheckerService = feedbackCheckerService;
            this.openAIService = openAIService;
        }

        public async ValueTask ProcessFeedbackAsync(Essay essay)
        {
            try
            {
                var feedback = await EnsureFeedbackAsync(essay);
                await this.feedbackEventService.PublishFeedbackAsync(feedback);

                var telegramUser = this.telegramUserService
                    .RetrieveAllTelegramUsers().FirstOrDefault(t => t.TelegramUserName == essay.TelegramUserName);

                await ModifyFeedbackCheckerAsync(essay);

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUser.TelegramId,
                    message: $"Feedback is done\nUser: {telegramUser.TelegramUserName}");
            }
            catch (Exception ex)
            {
                await this.telegramService.SendMessageAsync(
                    1924521160, $"Error at process feedback: {ex.Message}\nUser: {essay.TelegramUserName}");
            }
        }

        private async ValueTask<Feedback> EnsureFeedbackAsync(Essay essay)
        {
            var messageForAI = "You are IELTS Writing examiner. Give detailed IELTS feedback" +
                                "based on marking criteria of IELTS and give me overall Band.";

            var content = await this.openAIService.AnalizeRequestAsync(essay.Content, messageForAI);

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

        private async Task ModifyFeedbackCheckerAsync(Essay essay)
        {
            var feedbackChecker = this.feedbackCheckerService.RetrieveAllFeedbackCheckers()
                .FirstOrDefault(f => f.TelegramUserName == essay.TelegramUserName);

            feedbackChecker.State = true;
            await this.feedbackCheckerService.ModifyFeedbackCheckerAsync(feedbackChecker);
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
