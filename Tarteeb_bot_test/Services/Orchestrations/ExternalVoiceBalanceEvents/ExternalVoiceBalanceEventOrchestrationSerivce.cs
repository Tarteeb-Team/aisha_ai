using System.Threading.Tasks;
using Tarteeb_bot_test.Models.ExternalSpeechs;
using Tarteeb_bot_test.Models.ExternalVoices;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Tarteeb_bot_test.Services.Foundations.Levents.ExternalSpeechs;
using Tarteeb_bot_test.Services.Foundations.Levents.TelegramEvents;
using Tarteeb_bot_test.Services.Processings.Balances;
using Telegram.Bot.Types;

namespace Tarteeb_bot_test.Services.Orchestrations.ExternalVoiceBalanceEvents
{
    public class ExternalVoiceBalanceEventOrchestrationSerivce : IExternalVoiceBalanceEventOrchestrationSerivce
    {
        private readonly IBalanceProcessingService balanceProcessingService;
        private readonly ITelegramUserMessageEventService telegramUserMessageEventService;
        private readonly IExternalVoiceEventService externalSpeechEventService;

        public ExternalVoiceBalanceEventOrchestrationSerivce(
            IBalanceProcessingService balanceProcessingService,
            ITelegramUserMessageEventService telegramUserMessageEventService,
            IExternalVoiceEventService externalSpeechEventService)
        {
            this.balanceProcessingService = balanceProcessingService;
            this.telegramUserMessageEventService = telegramUserMessageEventService;
            this.externalSpeechEventService = externalSpeechEventService;
        }

        public void ListenTelegramUserMessageVoice()
        {
            this.telegramUserMessageEventService
                .ListenToTelegramUserMessageEvent(ProcessVerifyBalanceAsync);
        }

        private async ValueTask ProcessVerifyBalanceAsync(TelegramUserMessage telegramUserMessage)
        {
            var IsStateOfBalance = await this.balanceProcessingService.ReturnTrue();

            if (IsStateOfBalance is true)
            {
                ExternalVoice externalSpeech =
                    PopulateExternalSpeech(telegramUserMessage.Message.Voice);

                await this.externalSpeechEventService.PublishExternalVoiceAsync(externalSpeech);
            }
        }

        private ExternalVoice PopulateExternalSpeech(Voice voice)
        {
            return new ExternalVoice
            {
                FileUniqieId = voice.FileUniqueId,
                FileId = voice.FileId,
                Duration = voice.Duration,
                FileSize = voice.FileSize
            };
        }
    }
}
