using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace aisha_ai.Brokers.Blobs
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
