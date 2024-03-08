using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Essays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Essays;

public partial class EssayServiceTests
{
    [Fact]
    public async Task ShouldAddEssayAsync()
    {
        // given
        Essay randomEssay = CreateRandomEssay();
        Essay inputEssay = randomEssay;
        Essay persistedEssay = inputEssay;
        Essay expectedEssay = persistedEssay.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.InsertEssayAsync(inputEssay))
                .ReturnsAsync(persistedEssay);

        // when
        Essay actualEssay = await
            this.essayService.AddEssayAsync(inputEssay);

        // then
        actualEssay.Should().BeEquivalentTo(expectedEssay);

        this.storageBrokerMock.Verify(broker =>
            broker.InsertEssayAsync(inputEssay), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}