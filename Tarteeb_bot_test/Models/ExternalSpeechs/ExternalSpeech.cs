using System.IO;
using System.Reflection.PortableExecutable;

namespace Tarteeb_bot_test.Models.ExternalSpeechs
{
    public class ExternalSpeech
    {
        public string ExternalId { get; set; }
        public int Duration { get; set; }
        public long? FileSize { get; set; }
        public Stream Speech { get; set; }
    }
}
