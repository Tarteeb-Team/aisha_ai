using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using Tarteeb_bot_test.Models.TelegramUserMessages;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Tarteeb_bot_test.Services.Foundations.Telegrams
{
    public interface ITelegramService
    {
        void RegisterTelegramEventHandler(Func<TelegramUserMessage, ValueTask> eventHandler);
        ValueTask SendMessageAsync(long userTelegramId,
           string message,
           int? replyToMessageId = null,
           ParseMode? parseMode = null,
           IReplyMarkup? replyMarkup = null);

        ValueTask<Telegram.Bot.Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken = default(CancellationToken));
        ValueTask DownloadFileAsync(string filePath, Stream stream, CancellationToken cancellationToken = default(CancellationToken));
    }
}
