using System.IO;
using System.Threading.Tasks;
using aisha_ai.Brokers.Blobs;

namespace aisha_ai.Services.Foundations.Bloobs
{
    public class BlobService : IBlobService
    {
        private readonly IBlobBroker blobBroker;

        public BlobService(IBlobBroker blobBroker) =>
            this.blobBroker = blobBroker;

        public async Task UploadSpeechAsync(Stream stream, string fileName) =>
            await this.blobBroker.UploadSpeechAsync(stream, fileName);

        public async Task RemoveSpeechAsync(string fileName) =>
            await this.blobBroker.DeleteBlobAsync(fileName);

        public async Task<Stream> DownloadSpeechAsync(string fileName) =>
            await this.blobBroker.DownloadSpeechAsync(fileName);

        public async Task<bool> CheckIfBlobExistsAsync(string fileName) =>
            await this.blobBroker.CheckIfBlobExistsAsync(fileName);
    }
}
