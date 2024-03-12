using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers;
using Moq;
using Tynamix.ObjectFiller;

namespace aisha_ai_unit_tests.Services.Foundations.FeedbackCheckers
{
    public partial class FeedbackCheckerServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IFeedbackCheckerService feedbackCheckerService;

        public FeedbackCheckerServiceTests()
        {
            storageBrokerMock = new Mock<IStorageBroker>();

            feedbackCheckerService = new FeedbackCheckerService(
                storageBroker: storageBrokerMock.Object);
        }

        private static Filler<FeedbackChecker> CreateFeedbackCheckerFiller() =>
        new Filler<FeedbackChecker>();

        private static FeedbackChecker CreateRandomFeedbackChecker() =>
            CreateFeedbackCheckerFiller().Create();
    }
}
