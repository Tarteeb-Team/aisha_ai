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
        public async Task ShouldAddFeedbackCheckerAsync()
        {
            // given
            FeedbackChecker randomFeedbackChecker = CreateRandomFeedbackChecker();
            FeedbackChecker inputFeedbackChecker = randomFeedbackChecker;
            FeedbackChecker persistedFeedbackChecker = inputFeedbackChecker;
            FeedbackChecker expectedFeedbackChecker = persistedFeedbackChecker.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertFeedbackCheckerAsync(inputFeedbackChecker))
                .ReturnsAsync(persistedFeedbackChecker);

            // when
            FeedbackChecker actualFeedbackChecker =
                await this.feedbackCheckerService.AddFeedbackCheckerAsync(inputFeedbackChecker);

            // then
            actualFeedbackChecker.Should().BeEquivalentTo(expectedFeedbackChecker);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFeedbackCheckerAsync(inputFeedbackChecker),
                Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
