using System.Threading.Tasks;

namespace Tarteeb_bot_test.Services.Processings.TelegramUsers
{
    public class TelegramUserProcessingSerivce : ITelegramUserProcessingSerivce
    {
        public Task<bool> ReturnTrue()
        {
            return Task.FromResult(true);
        }

    }
}
