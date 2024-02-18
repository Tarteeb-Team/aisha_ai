using System.Threading.Tasks;

namespace Tarteeb_bot_test.Services.Processings.Users
{
    public class UserProcessingService : IUserProcessingService
    {
        public Task<bool> ReturnTrue()
        {
            return Task.FromResult(true);
        }
    }
}
