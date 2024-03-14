using System.Threading.Tasks;
using aisha_ai.Services.EssayServices.Orchestrations.SendToTelegramMessages;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinishEssayController : RESTFulController
    {
        private readonly ISendToTelegramMessageOrcherstrationService sendToTelegramMessageOrcherstrationService;

        public FinishEssayController(
            ISendToTelegramMessageOrcherstrationService sendToTelegramMessageOrcherstrationService)
        {
            this.sendToTelegramMessageOrcherstrationService = sendToTelegramMessageOrcherstrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> SendEssayMessageAsync(string telegramUserName)
        {
            await this.sendToTelegramMessageOrcherstrationService
                .SendToTelegramEssayOverralMessageAsync(telegramUserName);

            return Ok();
        }
    }
}
