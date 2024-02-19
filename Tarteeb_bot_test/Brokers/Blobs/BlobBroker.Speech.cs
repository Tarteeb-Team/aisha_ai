using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace Tarteeb_bot_test.Brokers.Blobs
{
    public partial class BlobBroker
    {
        private const string speechContainerName = "speechs";

        public async Task UploadSpeechAsync(MemoryStream memoryStream, string speechId)
        {
            BlobContainerClient container = CreateBlobServiceClient(speechContainerName);
            BlobClient blob = container.GetBlobClient(speechId);

            await blob.UploadAsync(memoryStream);
        }
    }
}
