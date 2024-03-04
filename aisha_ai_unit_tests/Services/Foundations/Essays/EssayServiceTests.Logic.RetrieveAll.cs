using System.Linq;
using System.Threading.Tasks;
using aisha_ai.Models.Essays;
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
            broker.RetrieveAllEssays())
                .Returns(persistedEssays);
        
        // when
        IQueryable<Essay> actualEssays = 
            this.essayService.RetrieveAllEssays();
        
        // then
        actualEssays.Should().BeEquivalentTo(expectedEssays);
        
        this.storageBrokerMock.Verify(broker =>
            broker.RetrieveAllEssays(), Times.Once);
        
        this.storageBrokerMock.VerifyNoOtherCalls();
    }
}