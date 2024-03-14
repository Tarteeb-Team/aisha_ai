using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Feedbacks
{
    public partial class FeedbackServiceTests
    {
        [Fact]
        public async Task ShouldModifyFeedbackAsync()
        {
            // given
            Feedback randomFeedback = CreateRandomFeedback();
            Feedback inputFeedback = randomFeedback;
            Feedback expectedFeedback = inputFeedback.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateFeedbackAsync(inputFeedback))
                    .ReturnsAsync(expectedFeedback);

            // when
            Feedback actualFeedback =
                await this.feedbackService.ModifyFeedbackAsync(inputFeedback);

            // then
            actualFeedback.Should().BeEquivalentTo(expectedFeedback);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateFeedbackAsync(inputFeedback), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
