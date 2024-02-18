using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;

namespace Tarteeb_bot_test.Brokers.Telegrams
{
    public interface ITelegramBroker
    {
        ValueTask SendTextMessageAsync(long userTelegramId,
           string message,
           int? replyToMessageId = null,
           ParseMode? parseMode = null,
           IReplyMarkup? replyMarkup = null);

        ValueTask DeleteWebhookAsync(CancellationToken cancellationToken);
        void RegisterTelegramEventHandler(Func<Update, ValueTask> eventHandler);
        ValueTask<Telegram.Bot.Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken = default(CancellationToken));
        ValueTask DownloadFileAsync(string filePath, Stream stream, CancellationToken cancellationToken = default(CancellationToken));
    }
}
