using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Chekers;
using aisha_ai.Services.Foundations.Checkers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckerController : RESTFulController
    {
        private readonly ICheckerService checkerService;

        public CheckerController(ICheckerService checkerService)
        {
            this.checkerService = checkerService;
        }

        [HttpGet]
        public ActionResult<bool> GetChecker(string telegramUserName)
        {
            try
            {
                Checker checker = this.checkerService.RetrieveAllCheckers()
                .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

                bool state = false;

                if (checker.State is true)
                    state = true;

                return Ok(state);
            }
            catch (Exception)
            {
                return Ok(null);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult> PutChekerAsync(string telegramUserName, bool state)
        {
            try
            {
                Checker checker = this.checkerService.RetrieveAllCheckers()
                    .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

                checker.State = state;
                await this.checkerService.ModifyCheckerAsync(checker);

                return Ok();
            }
            catch (Exception)
            {
                return Ok(null);
            }

        }
    }
}
