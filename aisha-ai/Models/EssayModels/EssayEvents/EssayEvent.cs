using aisha_ai.Models.EssayModels.Essays;
using aisha_ai.Models.EssayModels.TelegramUsers;

namespace aisha_ai.Models.EssayModels.EssayEvents
{
    public class EssayEvent
    {
        public TelegramUser TelegramUser { get; set; }
        public Essay Essay { get; set; }
    }
}
