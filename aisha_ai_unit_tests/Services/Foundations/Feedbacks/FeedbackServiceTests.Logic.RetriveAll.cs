using System.Linq;
using aisha_ai.Models.EssayModels.Feedbacks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Feedbacks
{
    public partial class FeedbackServiceTests
    {
        [Fact]
        public async void ShouldRetrieveAllFeedbacks()
        {
            // given
            IQueryable<Feedback> randomFeedbacks = CreateRandomFeedbacks();
            IQueryable<Feedback> inputFeedbacks = randomFeedbacks;
            IQueryable<Feedback> persistedFeedbacks = inputFeedbacks;
            IQueryable<Feedback> expectedFeedbacks = persistedFeedbacks.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllFeedbacks())
                    .Returns(persistedFeedbacks);

            // when
            IQueryable<Feedback> actualFeedbacks =
                this.feedbackService.RetrieveAllFeedbacks();

            // then
            actualFeedbacks.Should().BeEquivalentTo(expectedFeedbacks);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllFeedbacks(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
