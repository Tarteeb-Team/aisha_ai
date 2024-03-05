using System;
using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.TelegramUsers;
using aisha_ai.Services.Foundations.TelegramUsers;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace aisha_ai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramUserController : RESTFulController
    {
        private readonly ITelegramUserService telegramUserService;

        public TelegramUserController(ITelegramUserService telegramUserService) =>
            this.telegramUserService = telegramUserService;

        [HttpGet]
        public ActionResult<IQueryable<TelegramUser>> GetTelegramUsers()
        {
            IQueryable<TelegramUser> telegramUsers =
                this.telegramUserService.RetrieveAllTelegramUsers();

            return Ok(telegramUsers);
        }

        [HttpDelete]
        public async ValueTask<ActionResult<bool>> DeleteTelegramUser(string telegramUserName)
        {
            try
            {
                await this.telegramUserService.RemoveTelegramUserAsync(telegramUserName);

                return Ok(true);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }
    }
}
