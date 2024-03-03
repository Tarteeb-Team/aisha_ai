using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Brokers.Telegrams
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
