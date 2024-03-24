using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Services.Foundations.Telegrams;
using aisha_ai.Services.Foundations.TelegramUsers;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeeches;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbacks;
using Microsoft.AspNetCore.Hosting;

namespace aisha_ai.Services.SpeechServices.SendToTelegramMessages
{
    public class SendSpeechToTelegramMessageOrcherstrationService : ISendSpeechToTelegramMessageOrcherstrationService
    {
        private readonly ITelegramUserService telegramUserService;
        private readonly ITelegramService telegramService;
        private readonly IImprovedSpeechService improvedSpeechService;
        private readonly ISpeechFeedbackService speechFeedbackService;
        private readonly string wwwRootPath;

        public SendSpeechToTelegramMessageOrcherstrationService(
            ITelegramUserService telegramUserService,
            ITelegramService telegramService,
            IWebHostEnvironment webHostEnvironment,
            IImprovedSpeechService improvedSpeechService,
            ISpeechFeedbackService speechFeedbackService)
        {
            this.telegramUserService = telegramUserService;
            this.telegramService = telegramService;
            this.wwwRootPath = webHostEnvironment.WebRootPath;
            this.improvedSpeechService = improvedSpeechService;
            this.speechFeedbackService = speechFeedbackService;
        }

        public async ValueTask SendToTelegramSpeechOverralMessageAsync(string telegramUserName)
        {
            var telegramUser = this.telegramUserService
                .RetrieveAllTelegramUsers().FirstOrDefault(t => t.TelegramUserName == telegramUserName);

            if (telegramUser != null)
            {
                string audioFileName = $"{telegramUserName}.IS.wav";
                string audioFilePath = Path.Combine(wwwRootPath, audioFileName);

                var improvedSpeech = this.improvedSpeechService.RetrieveAllImprovedSpeechs()
                    .FirstOrDefault(i => i.TelegramUserName == telegramUserName);

                var speechFeedback = this.speechFeedbackService.RetrieveAllSpeechFeedbacks()
                    .FirstOrDefault(s => s.TelegramUserName == telegramUserName);

                if (System.IO.File.Exists(audioFilePath))
                {
                    using (var fileStream = System.IO.File.OpenRead(audioFilePath))
                    {
                        await this.telegramService.SendAudioAsync(
                            userTelegramId: telegramUser.TelegramId,
                            message: $"Improved speech voice 📌",
                            audio: Telegram.Bot.Types.InputFile.FromStream(fileStream));

                        await this.telegramService.SendMessageAsync(
                            userTelegramId: telegramUser.TelegramId,
                            message: $"Improved speech text 📌\n\n{improvedSpeech.Content}");

                        fileStream.Dispose();
                    }

                    File.Delete(audioFilePath);
                }
            }
        }
    }
}
