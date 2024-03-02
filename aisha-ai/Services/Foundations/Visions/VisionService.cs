using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using aisha_ai.Brokers.Visions;
using Azure.AI.FormRecognizer.Models;

namespace aisha_ai.Services.Foundations.Visions
{
    public class VisionService : IVisionService
    {
        private IVisionBroker visionBroker;

        public VisionService(IVisionBroker visionBroker) =>
            this.visionBroker = visionBroker;

        public async ValueTask<string> ExtractTextAsync(Stream imageStream)
        {
            try
            {
                List<FormPage> pages = await this.visionBroker.ExtractTextAsync(imageStream);

                var essayText = new StringBuilder();

                foreach (FormPage page in pages)
                {
                    foreach (FormLine line in page.Lines)
                    {
                        essayText.AppendLine(line.Text);
                    }
                }

                return essayText.ToString();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
