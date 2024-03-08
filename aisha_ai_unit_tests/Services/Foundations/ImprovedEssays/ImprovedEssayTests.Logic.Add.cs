using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.ImprovedEssays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.ImprovedEssays
{
    public partial class ImprovedEssayTests
    {
        [Fact]
        public async Task ShouldAddImprovedEssayAsync()
        {
            // given
            ImprovedEssay randomEssay = CreateRandomImprovedEssay();
            ImprovedEssay inputEssay = randomEssay;
            ImprovedEssay persistedEssay = inputEssay;
            ImprovedEssay expectedEssay = persistedEssay.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertImprovedEssayAsync(inputEssay))
                    .ReturnsAsync(expectedEssay);

            // when
            ImprovedEssay actualEssay =
                await this.improvedEssayService.AddImprovedEssayAsync(inputEssay);

            // then
            actualEssay.Should().BeEquivalentTo(expectedEssay);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertImprovedEssayAsync(inputEssay), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
