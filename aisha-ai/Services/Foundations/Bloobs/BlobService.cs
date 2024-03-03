using System.IO;
using System.Threading.Tasks;
using aisha_ai.Brokers.Blobs;

namespace aisha_ai.Services.Foundations.Bloobs
{
    public class BlobService : IBlobService
    {
        private readonly IBlobBroker blobBroker;

        public BlobService(IBlobBroker blobBroker)
        {
            this.blobBroker = blobBroker;
        }

        public async Task UploadSpeechAsync(MemoryStream memoryStream, string fileName) =>
            await this.blobBroker.UploadSpeechAsync(memoryStream, fileName);
    }
}
