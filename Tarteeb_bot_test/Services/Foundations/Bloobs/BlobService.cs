using System.IO;
using System.Threading.Tasks;
using Tarteeb_bot_test.Brokers.Blobs;

namespace Tarteeb_bot_test.Services.Foundations.Bloobs
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
