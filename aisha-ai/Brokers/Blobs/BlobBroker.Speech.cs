using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace aisha_ai.Brokers.Blobs
{
    public partial class BlobBroker
    {
        private const string speechContainerName = "audio";

        public async Task UploadSpeechAsync(Stream stream, string fileName)
        {
            BlobContainerClient container = CreateBlobServiceClient(speechContainerName);
            BlobClient blob = container.GetBlobClient(fileName);

            await blob.UploadAsync(stream);
        }

        public async Task DeleteBlobAsync(string fileName)
        {
            BlobContainerClient container = CreateBlobServiceClient(speechContainerName);
            BlobClient blob = container.GetBlobClient(fileName);

            await blob.DeleteIfExistsAsync();
        }

        public async Task<bool> CheckIfBlobExistsAsync(string fileName)
        {
            BlobContainerClient container = CreateBlobServiceClient(speechContainerName);
            BlobClient blob = container.GetBlobClient(fileName);

            return await blob.ExistsAsync();
        }
    }
}
