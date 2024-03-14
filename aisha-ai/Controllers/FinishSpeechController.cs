using System.Threading.Tasks;
using aisha_ai.Services.SpeechServices.SendToTelegramMessages;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinishSpeechController : RESTFulController
    {
        private readonly ISendSpeechToTelegramMessageOrcherstrationService sendSpeechToTelegramMessageOrcherstrationService;

        public FinishSpeechController(ISendSpeechToTelegramMessageOrcherstrationService sendSpeechToTelegramMessageOrcherstrationService)
        {
            this.sendSpeechToTelegramMessageOrcherstrationService = sendSpeechToTelegramMessageOrcherstrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> SendSpeechMessageAsync(string telegramUserName)
        {
            await this.sendSpeechToTelegramMessageOrcherstrationService
                .SendToTelegramSpeechOverralMessageAsync(telegramUserName);

            return Ok();
        }
    }
}
