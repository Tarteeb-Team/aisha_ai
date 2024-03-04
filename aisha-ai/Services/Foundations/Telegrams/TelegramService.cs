﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using aisha_ai.Brokers.Telegrams;
using aisha_ai.Models.TelegramUserMessages;
using aisha_ai.Models.TelegramUsers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace aisha_ai.Services.Foundations.Telegrams
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
                    TelegramUserName = update.Message.From.Username,
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

        public async ValueTask SendAudioAsync(
            long userTelegramId,
            string message,
            Telegram.Bot.Types.InputFile audio)
        {
            await this.telegramBroker.SendAudioAsync(
                 userTelegramId,
                 message,
                 audio);
        }

        public async ValueTask<Telegram.Bot.Types.File> GetFileAsync(string fileId, CancellationToken cancellationToken = default) =>
            await this.telegramBroker.GetFileAsync(fileId, cancellationToken);

        public async ValueTask DownloadFileAsync(string filePath, Stream stream, CancellationToken cancellationToken = default(CancellationToken)) =>
            await this.telegramBroker.DownloadFileAsync(filePath, stream, cancellationToken);
    }
}