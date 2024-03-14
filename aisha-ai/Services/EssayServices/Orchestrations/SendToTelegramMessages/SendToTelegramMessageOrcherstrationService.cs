using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Services.EssayServices.Foundations.ImprovedEssays;
using aisha_ai.Services.EssayServices.Orchestrations.SendToTelegramMessages;
using aisha_ai.Services.Foundations.Feedbacks;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using Microsoft.AspNetCore.Hosting;
using Telegram.Bot.Types;

namespace aisha_ai.Services.Orchestrations.SendToTelegramMessages
{
    public class SendToTelegramMessageOrcherstrationService : ISendToTelegramMessageOrcherstrationService
    {
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;
        private readonly string wwwRootPath;
        private readonly IFeedbackService feedbackService;
        private readonly IImprovedEssayService improvedEssayService;

        public SendToTelegramMessageOrcherstrationService(
            ITelegramService telegramService,
            ITelegramUserService telegramUserService,
            IWebHostEnvironment webHostEnvironment,
            IFeedbackService feedbackService,
            IImprovedEssayService improvedEssayService)
        {
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
            this.wwwRootPath = webHostEnvironment.WebRootPath;
            this.feedbackService = feedbackService;
            this.improvedEssayService = improvedEssayService;
        }

        public async ValueTask SendToTelegramEssayOverralMessageAsync(string telegramUserName)
        {
            var telegramUser = this.telegramUserService
                .RetrieveAllTelegramUsers().FirstOrDefault(t => t.TelegramUserName == telegramUserName);

            if (telegramUser != null)
            {
                string audioFileName = $"{telegramUserName}.wav";
                string audioFilePath = Path.Combine(wwwRootPath, audioFileName);

                var feedback = this.feedbackService.RetrieveAllFeedbacks()
                    .FirstOrDefault(f => f.TelegramUserName == telegramUserName);

                var improvedEssay = this.improvedEssayService.RetrieveAllImprovedEssays()
                    .FirstOrDefault(i => i.TelegramUserName == telegramUserName);

                if (System.IO.File.Exists(audioFilePath))
                {
                    using (var fileStream = System.IO.File.OpenRead(audioFilePath))
                    {
                        await this.telegramService.SendAudioAsync(
                            userTelegramId: telegramUser.TelegramId,
                            message: $"Feedback voice 📌",
                            audio: InputFile.FromStream(fileStream));

                        await this.telegramService.SendMessageAsync(
                            userTelegramId: telegramUser.TelegramId,
                            message: $"Feedback text 📌\n\n{feedback.Content}");

                        await this.telegramService.SendMessageAsync(
                            userTelegramId: telegramUser.TelegramId,
                            message: $"Improved essay 📝\n\n{improvedEssay.Content}");
                    }
                }
                System.IO.File.Delete(audioFilePath);
            }
        }
    }
}
