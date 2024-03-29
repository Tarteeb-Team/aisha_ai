using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Essays;

public partial class EssayServiceTests
{
    [Fact]
    public async Task ShouldModifyEssayAsync()
    {
        // given
        Essay randomEssay = CreateRandomEssay();
        Essay inputEssay = randomEssay;
        Essay expectedEssay = inputEssay.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.UpdateEssayAsync(inputEssay))
                .ReturnsAsync(expectedEssay);

        // when
        Essay actualEssay =
            await this.essayService.ModifyEssayAsync(inputEssay);

        // then
        actualEssay.Should().BeEquivalentTo(expectedEssay);

        this.storageBrokerMock.Verify(broker =>
            broker.UpdateEssayAsync(inputEssay), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}