using System.Threading.Tasks;
using aisha_ai.Models.Feedbacks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Feedbacks
{
    public partial class FeedbackServiceTests
    {
        [Fact]
        public async Task ShouldRemoveFeedbackAsync()
        {
            // given
            Feedback randomFeedback = CreateRandomFeedback();
            Feedback expectedFeedback = randomFeedback.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteFeedbackAsync(expectedFeedback))
                    .ReturnsAsync(expectedFeedback);

            // when
            Feedback actualFeedback = await this.feedbackService
                .RemoveFeedbackAsync(randomFeedback);

            //then
            actualFeedback.Should().BeEquivalentTo(expectedFeedback);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteFeedbackAsync(randomFeedback), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
