using System.Linq;
using aisha_ai.Models.ImprovedEssays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.ImprovedEssays
{
    public partial class ImprovedEssayTests
    {
        [Fact]
        public async void ShouldRetrieveAllImprovedEssays()
        {
            // given
            IQueryable<ImprovedEssay> randomImprovedEssays = CreateRandomImprovedEssays();
            IQueryable<ImprovedEssay> storageImprovedEssays = randomImprovedEssays;
            IQueryable<ImprovedEssay> externalImprovedEssays = storageImprovedEssays.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.RetrieveAllImprovedEssays())
                    .Returns(storageImprovedEssays);

            // when
            IQueryable<ImprovedEssay> actualImprovedEssays =
                this.improvedEssayService.RetrieveAllImprovedEssays();

            // then
            actualImprovedEssays.Should().BeEquivalentTo(externalImprovedEssays);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllFeedbacks(), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
