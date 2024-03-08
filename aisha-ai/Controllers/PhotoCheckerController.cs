using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Services.Foundations.PhotoCheckers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoCheckerController : RESTFulController
    {
        private readonly IPhotoCheckersService checkerService;

        public PhotoCheckerController(IPhotoCheckersService checkerService) =>
            this.checkerService = checkerService;

        [HttpGet]
        public ActionResult<bool> GetChecker(string telegramUserName)
        {
            try
            {
                var checker = this.checkerService.RetrieveAllPhotoCheckers()
                    .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

                return Ok(checker.State);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult> PutChekerAsync(string telegramUserName, bool state)
        {
            try
            {
                var checker = this.checkerService.RetrieveAllPhotoCheckers()
                    .FirstOrDefault(e => e.TelegramUserName == telegramUserName);

                checker.State = state;
                await this.checkerService.ModifyPhotoCheckerAsync(checker);

                return Ok();
            }
            catch (Exception)
            {
                return Ok(null);
            }
        }
    }
}
