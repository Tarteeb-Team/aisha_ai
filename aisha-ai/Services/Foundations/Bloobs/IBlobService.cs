using System.IO;
using System.Threading.Tasks;

namespace Tarteeb_bot_test.Services.Foundations.Bloobs
{
    public interface IBlobService
    {
        Task UploadSpeechAsync(MemoryStream memoryStream, string fileName);
    }
}
