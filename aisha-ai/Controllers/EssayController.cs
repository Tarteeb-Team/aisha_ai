using System.Linq;
using aisha_ai.Models.Essays;
using aisha_ai.Services.Foundations.Essays;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EssayController : RESTFulController
    {
        private readonly IEssayService essayService;

        public EssayController(IEssayService essayService) =>
            this.essayService = essayService;

        [HttpGet]
        public ActionResult<Essay> GetEssays(string telegramUserName)
        {
            Essay essay = this.essayService.RetrieveAllEssays()
                .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

            return Ok(essay);
        }
    }
}
