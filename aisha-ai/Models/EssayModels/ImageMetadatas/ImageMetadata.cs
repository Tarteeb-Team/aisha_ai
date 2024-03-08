using System.IO;
using aisha_ai.Models.EssayModels.TelegramUsers;

namespace aisha_ai.Models.EssayModels.ImageMetadatas
{
    public class ImageMetadata
    {
        public Stream ImageStream { get; set; }
        public TelegramUser TelegramUser { get; set; }
    }
}
