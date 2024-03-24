using System;
using System.IO;
using System.Threading.Tasks;
using aisha_ai.Brokers.Speeches;
using aisha_ai.Services.EssayServices.Foundations.Speeches;
using aisha_ai.Services.Foundations.Telegrams;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CognitiveServices.Speech;

namespace aisha_ai.Services.Foundations.Speeches
{
    public class SpeechService : ISpeechService
    {
        private readonly ISpeechBroker speechBroker;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITelegramService telegramService;
        private readonly string wwwRootPath;

        public SpeechService(
            ISpeechBroker speechBroker,
            IWebHostEnvironment webHostEnvironment,
            ITelegramService telegramService)
        {
            this.speechBroker = speechBroker;
            this.webHostEnvironment = webHostEnvironment;
            this.wwwRootPath = this.webHostEnvironment.WebRootPath;
            this.telegramService = telegramService;
        }

        public async ValueTask<string> CreateAndSaveSpeechAudioAsync(string text, string fileName)
        {
            try
            {
                text = text.Replace("\n", "").Replace("\t", "").Replace("*", "").Replace("\\\"", "").Replace("/", "");
                string audioFolderPath = Path.Combine(this.wwwRootPath, $"{fileName}.wav");

                SpeechSynthesisResult speechSynthesisResult =
                    await this.speechBroker.GetSpeechResultAsync(text);

                await SaveSpeechSynthesisResultToLocalDirectoryAsync(
                           speechSynthesisResult: speechSynthesisResult,
                           filePath: audioFolderPath);

                await this.telegramService.SendMessageAsync(1924521160, "Save speech is done");

                return audioFolderPath;
            }
            catch (Exception ex)
            {
                await this.telegramService
                    .SendMessageAsync(1924521160, $"Error at save speech: {ex.Message}");

                throw ex;
            }
        }


        private async Task SaveSpeechSynthesisResultToLocalDirectoryAsync(
        SpeechSynthesisResult speechSynthesisResult,
        string filePath)
        {
            if (speechSynthesisResult.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                try
                {
                    using (var audioStream = AudioDataStream.FromResult(speechSynthesisResult))
                    {
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }

                        await audioStream.SaveToWaveFileAsync(filePath);
                    }

                    speechSynthesisResult.Dispose();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
