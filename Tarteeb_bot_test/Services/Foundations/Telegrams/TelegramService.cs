using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using Tarteeb_bot_test.Brokers.Telegrams;
using Tarteeb_bot_test.Models.TelegramUsers;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Telegram.Bot.Types;

namespace Tarteeb_bot_test.Services.Foundations.Telegrams
{
    public class TelegramService : ITelegramService
    {
        private readonly ITelegramBroker telegramBroker;

        public TelegramService(ITelegramBroker telegramBroker)
        {
            this.telegramBroker = telegramBroker;
        }

        public void RegisterTelegramEventHandler(Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            this.telegramBroker.RegisterTelegramEventHandler(async message =>
                await ProcessTelegramTaskAsync(message, eventHandler));
        }

        private async ValueTask ProcessTelegramTaskAsync(Update update, Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            if (update.Type == UpdateType.Message)
            {
                var telegramUser = new TelegramUser
                {
                    Id = Guid.NewGuid(),
                    TelegramId = update.Message.From.Id,
                    Name = update.Message.From.FirstName
                };

                var telegramUserMessage = new TelegramUserMessage
                {
                    TelegramUser = telegramUser,
                    Message = update.Message
                };

                await eventHandler(telegramUserMessage);
            }
        }

        public async ValueTask SendMessageAsync(
            long userTelegramId,
            string message,
            int? replyToMessageId = null,
            ParseMode? parseMode = null,
            IReplyMarkup? replyMarkup = null)
        {
            await this.telegramBroker.SendTextMessageAsync(
                    userTelegramId: userTelegramId,
                    message: message,
                    replyToMessageId: replyToMessageId,
                    parseMode: parseMode,
                    replyMarkup: replyMarkup);

        }

        public async ValueTask<Telegram.Bot.Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken = default) =>
            await this.telegramBroker.GetFileAsync(fileId, cancellationToken);

        public async ValueTask DownloadFileAsync(string filePath, Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
            await this.telegramBroker.DownloadFileAsync(filePath, stream, cancellationToken);
    }
}
