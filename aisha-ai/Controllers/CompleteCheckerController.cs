using System.Threading.Tasks;
using aisha_ai.Services.Orchestrations.SendToTelegramMessages;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompleteCheckerController : RESTFulController
    {
        private readonly ISendToTelegramMessageOrcherstrationService sendToTelegramMessageOrcherstrationService;

        public CompleteCheckerController(
            ISendToTelegramMessageOrcherstrationService sendToTelegramMessageOrcherstrationService)
        {
            this.sendToTelegramMessageOrcherstrationService = sendToTelegramMessageOrcherstrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> PostChekerAsync(string telegramUserName)
        {
            await this.sendToTelegramMessageOrcherstrationService.SendToTelegramOverralMessageAsync(telegramUserName);

            return Ok();
        }
    }
}
