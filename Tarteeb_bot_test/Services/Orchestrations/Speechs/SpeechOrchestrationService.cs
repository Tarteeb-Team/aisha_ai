using System;
using System.IO;
using System.Threading.Tasks;
using Concentus.Oggfile;
using Concentus.Structs;
using Microsoft.VisualBasic;
using NAudio.Wave;
using Tarteeb_bot_test.Models.ExternalVoices;
using Tarteeb_bot_test.Services.Foundations.Bloobs;
using Tarteeb_bot_test.Services.Foundations.Levents.ExternalSpeechs;
using Tarteeb_bot_test.Services.Foundations.Telegrams;

namespace Tarteeb_bot_test.Services.Orchestrations.Speechs
{
    public class SpeechOrchestrationService : ISpeechOrchestrationService
    {
        private readonly IExternalVoiceEventService externalSpeechEventService;
        //private readonly IBlobService bloobService;
        private readonly ITelegramService telegramService;

        public SpeechOrchestrationService(
            IExternalVoiceEventService externalSpeechEventService,
            //IBlobService bloobService,
            ITelegramService telegramService)
        {
            this.externalSpeechEventService = externalSpeechEventService;
            //this.bloobService = bloobService;
            this.telegramService = telegramService;
        }

        public void ListenExternalVoice() =>
            this.externalSpeechEventService.ListenToExternalVoiceEvent(ProcessExternalVoiceAsync);

        private async ValueTask ProcessExternalVoiceAsync(ExternalVoice externalVoice)
        {
            var file = await telegramService.GetFileAsync(externalVoice.FileId);

            using (var stream = new MemoryStream())
            {
                await telegramService.DownloadFileAsync(file.FilePath, stream);
                stream.Position = 0;

                var voice = ReturningConvertOggToWav(stream, externalVoice.FileUniqieId);
            }
        }

        public string ReturningConvertOggToWav(Stream stream, string filePath)
        {
            using (MemoryStream pcmStream = new MemoryStream())
            {
                OpusDecoder decoder = OpusDecoder.Create(48000, 1);
                OpusOggReadStream oggIn = new OpusOggReadStream(decoder, stream);
                while (oggIn.HasNextPacket)
                {
                    short[] packet = oggIn.DecodeNextPacket();
                    if (packet != null)
                    {
                        foreach (short sample in packet)
                        {
                            var bytes = BitConverter.GetBytes(sample);
                            pcmStream.Write(bytes, 0, bytes.Length);
                        }
                    }
                }

                pcmStream.Position = 0;
                var wavStream = new RawSourceWaveStream(pcmStream, new WaveFormat(48000, 1));
                var sampleProvider = wavStream.ToSampleProvider();
                var userPath = filePath + ".wav";

                WaveFileWriter.CreateWaveFile16(userPath, sampleProvider);

                return userPath;
            }
        }
    }
}
