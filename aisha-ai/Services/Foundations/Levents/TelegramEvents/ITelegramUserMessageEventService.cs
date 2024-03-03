using System;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUserMessages;

namespace aisha_ai.Services.Foundations.Levents.TelegramEvents
{
    public interface ITelegramUserMessageEventService
    {
        ValueTask PublishTelegramUserMessageAsync(
           TelegramUserMessage telegramUserMessage,
           string eventName = null);

        void ListenToTelegramUserMessageEvent(
            Func<TelegramUserMessage, ValueTask> telegramUserMessageEventHandler,
            string eventName = null);
    }
}
