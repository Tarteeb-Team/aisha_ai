using System.Threading.Tasks;
using aisha_ai.Services.SpeechServices.Orcherstrations.Speeches;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechController : RESTFulController
    {
        private readonly ISpeechOrcherstrationService speechOrcherstrationService;

        public SpeechController(ISpeechOrcherstrationService speechOrcherstrationService) =>
            this.speechOrcherstrationService = speechOrcherstrationService;

        [HttpGet]
        public async ValueTask<ActionResult> GetSpeechFeedbackAsync(string blobName)
        {
            await this.speechOrcherstrationService.SpeechProcessAsync(blobName);

            return Ok();
        }
    }
}
