using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aisha_ai.Models.ImprovedEssays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.ImprovedEssays
{
    public partial class ImprovedEssayTests
    {
        [Fact]
        public async Task ShouldRemoveImprovedEssayAsync()
        {
            // given
            ImprovedEssay randomImprovedEssay = CreateRandomImprovedEssay();
            ImprovedEssay expectedImprovedEssay = randomImprovedEssay.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteImprovedEssayAsync(expectedImprovedEssay))
                    .ReturnsAsync(expectedImprovedEssay);

            // when
            ImprovedEssay actualImprovedEssay =
                await this.improvedEssayService.RemoveImprovedEssayAsync(expectedImprovedEssay);

            // then
            actualImprovedEssay.Should().BeEquivalentTo(expectedImprovedEssay);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteImprovedEssayAsync(expectedImprovedEssay), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();    
        }
    }
}
