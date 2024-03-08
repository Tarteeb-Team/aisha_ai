using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.TelegramUserMessages;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Services.Foundations.Telegrams
{
    public interface ITelegramService
    {
        void RegisterTelegramEventHandler(Func<TelegramUserMessage, ValueTask> eventHandler);
        ValueTask SendMessageAsync(long userTelegramId,
           string message,
           int? replyToMessageId = null,
           ParseMode? parseMode = null,
           IReplyMarkup? replyMarkup = null);

        ValueTask SendAudioAsync(
            long userTelegramId,
            string message,
            Telegram.Bot.Types.InputFile audio);

        ValueTask<Telegram.Bot.Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken = default(CancellationToken));
        ValueTask DownloadFileAsync(string filePath, Stream stream, CancellationToken cancellationToken = default(CancellationToken));
    }
}
