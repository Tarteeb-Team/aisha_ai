using System.Linq;
using aisha_ai.Models.ImprovedEssays;
using aisha_ai.Services.Foundations.ImprovedEssays;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImprovedEssayController : RESTFulController
    {
        private readonly IImprovedEssayService improvedEssayService;

        public ImprovedEssayController(IImprovedEssayService improvedEssayService)
        {
            this.improvedEssayService = improvedEssayService;
        }

        [HttpGet]
        public ActionResult<ImprovedEssay> GetImprovedEssay(string telegramUserName)
        {
            ImprovedEssay improvedEssay = this.improvedEssayService.RetrieveAllImprovedEssays()
                .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

            return Ok(improvedEssay);
        }
    }
}
