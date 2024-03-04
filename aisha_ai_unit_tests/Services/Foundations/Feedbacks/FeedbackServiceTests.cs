using System.Linq;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.Feedbacks;
using aisha_ai.Services.Foundations.Feedbacks;
using Moq;
using Tynamix.ObjectFiller;

namespace aisha_ai_unit_tests.Services.Foundations.Feedbacks
{
    public partial class FeedbackServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IFeedbackService feedbackService;

        public FeedbackServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.feedbackService = new FeedbackService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static Filler<Feedback> CreateFeedbackFiller() =>
            new Filler<Feedback>();

        private static Feedback CreateRandomFeedback() =>
            CreateFeedbackFiller().Create();

        private static IQueryable<Feedback> CreateRandomFeedbacks() =>
            CreateFeedbackFiller().Create(count: GetRandomNumber()).AsQueryable();
    }
}
