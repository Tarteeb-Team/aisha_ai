using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackCheckerController : RESTFulController
    {
        private readonly IFeedbackCheckerService feedbackCheckerService;

        public FeedbackCheckerController(IFeedbackCheckerService feedbackCheckerService) =>
            this.feedbackCheckerService = feedbackCheckerService;

        [HttpGet]
        public ActionResult<bool> GetChecker(string telegramUserName)
        {
            try
            {
                var feedbackChecker = this.feedbackCheckerService
                    .RetrieveAllFeedbackCheckers().First(e => e.TelegramUserName == telegramUserName);

                return Ok(feedbackChecker.State);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult> PutChekerAsync(string telegramUserName, bool state)
        {
            var feedbackChecker = this.feedbackCheckerService
                .RetrieveAllFeedbackCheckers().First(e => e.TelegramUserName == telegramUserName);

            feedbackChecker.State = state;
            await this.feedbackCheckerService.ModifyFeedbackCheckerAsync(feedbackChecker);

            return Ok();
        }
    }
}
