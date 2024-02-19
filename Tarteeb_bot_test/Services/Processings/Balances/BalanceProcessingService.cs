using System.Threading.Tasks;

namespace Tarteeb_bot_test.Services.Processings.Balances
{
    public class BalanceProcessingService : IBalanceProcessingService
    {
        public Task<bool> ReturnTrue()
        {
            return Task.FromResult(true);
        }
    }
}
