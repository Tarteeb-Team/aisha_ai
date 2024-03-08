using System.Linq;
using aisha_ai.Models.EssayModels.Essays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.Essays;

public partial class EssayServiceTests
{
    [Fact]
    public void ShouldRetrieveAllEssaysAsync()
    {
        // given
        IQueryable<Essay> randomEssays = CreateRandomEssays();
        IQueryable<Essay> inputEssays = randomEssays;
        IQueryable<Essay> persistedEssays = inputEssays;
        IQueryable<Essay> expectedEssays = persistedEssays.DeepClone();

        this.storageBrokerMock.Setup(broker =>
            broker.SelectAllEssays())
                .Returns(persistedEssays);

        // when
        IQueryable<Essay> actualEssays =
            this.essayService.RetrieveAllEssays();

        // then
        actualEssays.Should().BeEquivalentTo(expectedEssays);

        this.storageBrokerMock.Verify(broker =>
            broker.SelectAllEssays(), Times.Once);

        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}