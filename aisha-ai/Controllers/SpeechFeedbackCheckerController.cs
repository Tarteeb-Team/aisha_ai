using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Services.SpeechServices.Foundations.SpeechFeedbackCheckers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpeechFeedbackCheckerController : RESTFulController
    {
        private readonly ISpeechFeedbackCheckerService speechFeedbackCheckerService;

        public SpeechFeedbackCheckerController(ISpeechFeedbackCheckerService speechFeedbackCheckerService) =>
            this.speechFeedbackCheckerService = speechFeedbackCheckerService;

        [HttpGet]
        public ActionResult<bool> GetSpeechFeedbackChecker(string telegramUserName)
        {
            try
            {
                var speechFeedbackChecker = this.speechFeedbackCheckerService.RetrieveAllSpeechFeedbackCheckers()
                    .First(s => s.TelegramUserName == telegramUserName);

                return Ok(speechFeedbackChecker.State);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult> PutSpeechFeedbackCheckerAsync(string telegramUserName, bool state)
        {
            var speechFeedbackChecker = this.speechFeedbackCheckerService.RetrieveAllSpeechFeedbackCheckers()
                .First(s => s.TelegramUserName == telegramUserName);

            speechFeedbackChecker.State = state;
            await this.speechFeedbackCheckerService.ModifySpeechFeedbackCheckerAsync(speechFeedbackChecker);

            return Ok();
        }
    }
}
