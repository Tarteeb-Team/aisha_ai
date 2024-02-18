using System.Threading.Tasks;

namespace Tarteeb_bot_test.Services.Processings.TelegramUsers
{
    public interface ITelegramUserProcessingSerivce
    {
        Task<bool> ReturnTrue();
    }
}
