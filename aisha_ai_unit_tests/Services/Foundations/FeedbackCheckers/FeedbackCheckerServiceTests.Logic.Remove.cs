using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.FeedbackCheckers
{
    public partial class FeedbackCheckerServiceTests
    {
        [Fact]
        public async Task ShouldRemoveFeedbackCheckerAsync()
        {
            // given
            FeedbackChecker randomFeedbackChecker = CreateRandomFeedbackChecker();
            FeedbackChecker inputFeedbackChecker = randomFeedbackChecker;
            FeedbackChecker deletedFeedbackChecker = inputFeedbackChecker;
            FeedbackChecker expectedFeedbackChecker = deletedFeedbackChecker.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteFeedbackCheckerAsync(inputFeedbackChecker))
                    .ReturnsAsync(deletedFeedbackChecker);

            // when
            FeedbackChecker actualFeedbackChecker =
                await this.feedbackCheckerService.RemoveFeedbackCheckerAsync(inputFeedbackChecker);

            // then
            actualFeedbackChecker.Should().BeEquivalentTo(expectedFeedbackChecker);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteFeedbackCheckerAsync(inputFeedbackChecker), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
