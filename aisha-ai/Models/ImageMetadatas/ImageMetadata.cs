using System.IO;
using aisha_ai.Models.TelegramUsers;

namespace aisha_ai.Models.ImageMetadatas
{
    public class ImageMetadata
    {
        public Stream ImageStream { get; set; }
        public TelegramUser TelegramUser { get; set; }
    }
}
