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
        public async Task ShouldAddFeedbackAsync()
        {
            // given
            Feedback randomFeedback = CreateRandomFeedback();
            Feedback inputFeedback = randomFeedback;
            Feedback persistedFeedback = inputFeedback;
            Feedback expectedFeedback = persistedFeedback.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertFeedbackAsync(inputFeedback))
                    .ReturnsAsync(persistedFeedback);

            // when
            Feedback actualFeedback =
                await this.feedbackService.AddFeedbackAsync(inputFeedback);

            // then
            actualFeedback.Should().BeEquivalentTo(expectedFeedback);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertFeedbackAsync(inputFeedback), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
