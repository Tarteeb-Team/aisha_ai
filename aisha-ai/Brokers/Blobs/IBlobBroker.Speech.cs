using System.IO;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task UploadSpeechAsync(MemoryStream memoryStream, string speechId);
    }
}
