using System.IO;
using System.Threading.Tasks;

namespace aisha_ai.Services.Foundations.Bloobs
{
    public interface IBlobService
    {
        Task UploadSpeechAsync(Stream stream, string fileName);
        Task RemoveSpeechAsync(string fileName);
        Task<Stream> DownloadSpeechAsync(string fileName);
        Task<bool> CheckIfBlobExistsAsync(string fileName);
    }
}
