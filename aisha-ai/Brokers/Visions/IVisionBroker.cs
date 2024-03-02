using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.AI.FormRecognizer.Models;

namespace aisha_ai.Brokers.Visions
{
    public interface IVisionBroker
    {
        ValueTask<List<FormPage>> ExtractTextAsync(Stream stream);
    }
}
