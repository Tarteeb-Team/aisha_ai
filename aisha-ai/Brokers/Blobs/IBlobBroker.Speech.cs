using System.IO;
using System.Threading.Tasks;

namespace Tarteeb_bot_test.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task UploadSpeechAsync(MemoryStream memoryStream, string speechId);
    }
}
