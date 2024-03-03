using System.IO;
using System.Threading.Tasks;

namespace aisha_ai.Services.Foundations.Bloobs
{
    public interface IBlobService
    {
        Task UploadSpeechAsync(MemoryStream memoryStream, string fileName);
    }
}
