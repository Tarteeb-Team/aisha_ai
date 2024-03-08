using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Essays;

public partial class EssayServiceTests
{
    [Fact]
    public async Task ShouldRemoveEssayAsync()
    {
        // given
        Essay randomEssay = CreateRandomEssay();
        Essay inputEssay = randomEssay;
        Essay deletedEssay = inputEssay;
        Essay expectedEssay = deletedEssay.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.DeleteEssayAsync(inputEssay))
                .ReturnsAsync(deletedEssay);

        // when
        Essay actualEssay =
            await this.essayService.RemoveEssayAsync(inputEssay);

        // then
        actualEssay.Should().BeEquivalentTo(expectedEssay);

        this.storageBrokerMock.Verify(broker =>
            broker.DeleteEssayAsync(inputEssay), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}