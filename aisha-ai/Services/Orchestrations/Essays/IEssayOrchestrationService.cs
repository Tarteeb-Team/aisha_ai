using System;
using System.Threading.Tasks;
using aisha_ai.Models.Essays;

namespace Aisha.Core.Services.Orchestrations.Essays
{
    public interface IEssayOrchestrationService
    {
        void ListenImageMetadata(Func<Essay, ValueTask> essayAnalyseHandler);
    }
}
