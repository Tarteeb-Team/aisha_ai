using System;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;
using aisha_ai.Models.EssayModels.FeedbackCheckers;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.FeedbackCheckers
{
    public partial class FeedbackCheckerServiceTests
    {
        [Fact]
        public async Task ShouldModifyFeedbackCheckerAsync()
        {
            // given
            FeedbackChecker randomFeedbackChecker = CreateRandomFeedbackChecker();
            FeedbackChecker inputFeedbackChecker = randomFeedbackChecker;
            FeedbackChecker expectedFeedbackChecker = inputFeedbackChecker.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateFeedbackCheckerAsync(inputFeedbackChecker))
                    .ReturnsAsync(expectedFeedbackChecker);

            // when
            FeedbackChecker actualFeedbackChecker =
                await this.feedbackCheckerService.ModifyFeedbackCheckerAsync(inputFeedbackChecker);

            // then
            actualFeedbackChecker.Should().BeEquivalentTo(expectedFeedbackChecker);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateFeedbackCheckerAsync(inputFeedbackChecker), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
