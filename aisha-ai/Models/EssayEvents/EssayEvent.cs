using aisha_ai.Models.Essays;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Models.EssayEvents
{
    public class EssayEvent
    {
        public TelegramUser TelegramUser { get; set; }
        public Essay Essay { get; set; }
    }
}
