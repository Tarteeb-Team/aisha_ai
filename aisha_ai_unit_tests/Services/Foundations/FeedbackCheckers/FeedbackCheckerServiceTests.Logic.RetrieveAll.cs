using System.Linq;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.FeedbackCheckers
{
    public partial class FeedbackCheckerServiceTests
    {
        [Fact]
        public void ShouldRetrieveAllFeedbackCheckers()
        {
            // given
            IQueryable<FeedbackChecker> FeedbackChecker = CreateRandomFeedbackCheckers();
            IQueryable<FeedbackChecker> storageFeedbackChecker = FeedbackChecker;
            IQueryable<FeedbackChecker> externalFeedbackChecker = storageFeedbackChecker.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.RetrieveAllFeedbackCheckers()).Returns(storageFeedbackChecker);

            // when
            IQueryable<FeedbackChecker> actualFeedbackChecker =
                this.feedbackCheckerService.RetrieveAllFeedbackCheckers();

            // then
            actualFeedbackChecker.Should().BeEquivalentTo(externalFeedbackChecker);

            this.storageBrokerMock.Verify(broker =>
                broker.RetrieveAllFeedbackCheckers(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
