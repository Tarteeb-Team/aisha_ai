using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Brokers.Telegrams
{
    public class TelegramBroker : ITelegramBroker
    {
        private readonly ITelegramBotClient telegramBotClient;
        private static Func<Update, ValueTask> taskHandler;

        public TelegramBroker(IConfiguration configuration)
        {
            var token = configuration["Bot"];
            this.telegramBotClient = new TelegramBotClient(token);
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            this.telegramBotClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions);
        }

        private async Task HandleUpdateAsync(ITelegramBotClient telegramBotClient, Update update, CancellationToken ct)
        {
            try
            {
                await taskHandler(update);
            }
            catch (Exception ex)
            {

                Console.Write($"{ex}");
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        public void RegisterTelegramEventHandler(Func<Update, ValueTask> eventHandler) =>
            taskHandler = eventHandler;

        public async ValueTask DeleteWebhookAsync(CancellationToken cancellationToken) =>
            await this.telegramBotClient.DeleteWebhookAsync();

        public async ValueTask SendTextMessageAsync(
            long userTelegramId,
            string message,
            int? replyToMessageId = null,
            ParseMode? parseMode = null,
            IReplyMarkup? replyMarkup = null)
        {
            await telegramBotClient.SendTextMessageAsync(
                chatId: userTelegramId,
                text: message,
                parseMode: parseMode,
                replyToMessageId: replyToMessageId,
                replyMarkup: replyMarkup);
        }

        public async ValueTask<Telegram.Bot.Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken = default) =>
            await this.telegramBotClient.GetFileAsync(fileId, cancellationToken);

        public async ValueTask DownloadFileAsync(string filePath, Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
            await this.telegramBotClient.DownloadFileAsync(filePath, stream, cancellationToken);
    }
}
