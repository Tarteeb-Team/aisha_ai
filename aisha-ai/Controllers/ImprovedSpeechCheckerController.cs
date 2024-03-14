using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Services.SpeechServices.Foundations.ImprovedSpeechFeedbackCheckers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImprovedSpeechCheckerController : RESTFulController
    {
        private readonly IImprovedSpeechCheckerService improvedSpeechCheckerService;

        public ImprovedSpeechCheckerController(IImprovedSpeechCheckerService improvedSpeechCheckerService) =>
            this.improvedSpeechCheckerService = improvedSpeechCheckerService;

        [HttpGet]
        public ActionResult<bool> GetImprovedSpeechChecker(string telegramUserName)
        {
            try
            {
                var improvedSpeechChecker = this.improvedSpeechCheckerService.RetrieveAllImprovedSpeechCheckers()
                .First(s => s.TelegramUserName == telegramUserName);

                return Ok(improvedSpeechChecker.State);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult> PutImprovedSpeechCheckerAsync(string telegramUserName, bool state)
        {
            var improvedSpeechChecker = this.improvedSpeechCheckerService.RetrieveAllImprovedSpeechCheckers()
                .First(s => s.TelegramUserName == telegramUserName);

            improvedSpeechChecker.State = state;

            await this.improvedSpeechCheckerService
                .ModifyImprovedSpeechCheckerAsync(improvedSpeechChecker);

            return Ok();
        }
    }
}
