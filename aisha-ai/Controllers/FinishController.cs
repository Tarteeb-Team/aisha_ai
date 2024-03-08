﻿using System.Threading.Tasks;
using aisha_ai.Services.EssayServices.Orchestrations.SendToTelegramMessages;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinishController : RESTFulController
    {
        private readonly ISendToTelegramMessageOrcherstrationService sendToTelegramMessageOrcherstrationService;

        public FinishController(
            ISendToTelegramMessageOrcherstrationService sendToTelegramMessageOrcherstrationService)
        {
            this.sendToTelegramMessageOrcherstrationService = sendToTelegramMessageOrcherstrationService;
        }

        [HttpPost]
        public async ValueTask<ActionResult> PostChekerAsync(string telegramUserName)
        {
            await this.sendToTelegramMessageOrcherstrationService
                .SendToTelegramOverralMessageAsync(telegramUserName);

            return Ok();
        }
    }
}
