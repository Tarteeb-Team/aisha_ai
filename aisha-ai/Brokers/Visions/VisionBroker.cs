using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.VisionConfigurations;
using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Microsoft.Extensions.Configuration;

namespace aisha_ai.Brokers.Visions
{
    public class VisionBroker : IVisionBroker
    {
        private readonly AzureKeyCredential keyCredential;
        private readonly FormRecognizerClient formRecognizerClient;

        public VisionBroker(IConfiguration configuration)
        {
            var visionConfiguration = new VisionConfiguration();
            configuration.Bind(key: "VisionConfiguration", instance: visionConfiguration);
            keyCredential = new AzureKeyCredential(key: visionConfiguration.VisionKey);

            formRecognizerClient =
                new FormRecognizerClient(
                    endpoint: new Uri(visionConfiguration.VisionAddress),
                    credential: keyCredential);
        }

        public async ValueTask<List<FormPage>> ExtractTextAsync(Stream stream)
        {
            RecognizeContentOperation operation =
                await formRecognizerClient.StartRecognizeContentAsync(stream);

            await operation.WaitForCompletionAsync();
            FormPageCollection formPages = operation.Value;

            return formPages.ToList();
        }
    }
}
