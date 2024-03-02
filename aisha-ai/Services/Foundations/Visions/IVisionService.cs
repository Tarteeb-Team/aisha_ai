using System.IO;
using System.Threading.Tasks;

namespace aisha_ai.Services.Foundations.Visions
{
    public interface IVisionService
    {
        ValueTask<string> ExtractTextAsync(Stream imageStream);
    }
}
