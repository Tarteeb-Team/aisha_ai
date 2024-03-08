using System.Linq;
using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Services.Foundations.Feedbacks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : RESTFulController
    {
        private readonly IFeedbackService feedbackService;

        public FeedbackController(IFeedbackService feedbackService) =>
            this.feedbackService = feedbackService;

        [HttpGet]
        public ActionResult<Feedback> GetFeedback(string telegramUserName)
        {
            Feedback feedback = this.feedbackService.RetrieveAllFeedbacks()
                .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

            return Ok(feedback);
        }
    }
}
