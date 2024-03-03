using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace aisha_ai.Brokers.Blobs
{
    public partial class BlobBroker : IBlobBroker
    {
        private readonly IConfiguration configuration;

        public BlobBroker(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BlobContainerClient CreateBlobServiceClient(string containerName)
        {
            string connectionString =
                this.configuration.GetConnectionString(name: "BlobConnection");

            return new BlobContainerClient(connectionString, containerName);
        }
    }
}
