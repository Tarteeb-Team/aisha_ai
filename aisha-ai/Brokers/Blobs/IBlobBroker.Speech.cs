using System.IO;
using System.Threading.Tasks;

namespace aisha_ai.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task UploadSpeechAsync(Stream memoryStream, string speechId);
        Task DeleteBlobAsync(string fileName);
        Task<Stream> DownloadSpeechAsync(string fileName);
        Task<bool> CheckIfBlobExistsAsync(string fileName);
    }
}
