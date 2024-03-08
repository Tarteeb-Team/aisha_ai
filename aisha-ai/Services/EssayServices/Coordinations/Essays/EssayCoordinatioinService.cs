using System.Threading.Tasks;
using Aisha.Core.Services.Orchestrations.Essays;
using aisha_ai.Models.EssayModels.Essays;
using aisha_ai.Services.Orchestrations.Feedbacks;

namespace Aisha.Core.Services.Coordnations.Essays
{
    public class EssayCoordinatioinService : IEssayCoordinatioinService
    {
        private readonly IEssayOrchestrationService essayOrchestrationService;
        private readonly IFeedbackOrchestrationService feedbackOrchestrationService;

        public EssayCoordinatioinService(
            IEssayOrchestrationService essayOrchestrationService,
            IFeedbackOrchestrationService feedbackOrchestrationService)
        {
            this.essayOrchestrationService = essayOrchestrationService;
            this.feedbackOrchestrationService = feedbackOrchestrationService;
        }

        public void ListenEssay()
        {
            this.essayOrchestrationService.ListenImageMetadata(async (essay) =>
            {
                await ProcessEssayAsync(essay);
            });
        }

        private async Task ProcessEssayAsync(Essay essay) =>
            await this.feedbackOrchestrationService.ProcessFeedbackAsync(essay);
    }
}
