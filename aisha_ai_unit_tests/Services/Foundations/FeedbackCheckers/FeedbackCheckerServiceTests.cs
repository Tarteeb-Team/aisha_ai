using System;
using System.Linq;
using System.Linq.Expressions;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using aisha_ai.Services.EssayServices.Foundations.FeedbackCheckers;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

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

        private static int GetRandomNUmber() =>
        new IntRange(min: 2, max: 9).GetValue();

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static Filler<FeedbackChecker> CreateFeedbackCheckerFiller() =>
        new Filler<FeedbackChecker>();
        private IQueryable<FeedbackChecker> CreateRandomFeedbackCheckers()
        {
            return CreateFeedbackCheckerFiller()
                .Create(count: GetRandomNUmber()).AsQueryable();
        }

        private static FeedbackChecker CreateRandomFeedbackChecker() =>
            CreateFeedbackCheckerFiller().Create();
    }
}
