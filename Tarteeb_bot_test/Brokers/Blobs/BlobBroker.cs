using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Tarteeb_bot_test.Brokers.Blobs
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
