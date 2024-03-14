using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aisha_ai.Models.EssayModels.Feedbacks;
using aisha_ai.Models.EssayModels.ImprovedEssays;
using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace aisha_ai_unit_tests.Services.Foundations.ImprovedEssays
{
    public partial class ImprovedEssayTests
    {
        [Fact]
        public async Task ShouldImprovedEssayAsync()
        {
            // given
            ImprovedEssay randomImprovedEssay = CreateRandomImprovedEssay();
            ImprovedEssay inpuImprovedEssay = randomImprovedEssay;
            ImprovedEssay expectedImprovedEssay = inpuImprovedEssay.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateImprovedEssayAsync(inpuImprovedEssay))
                    .ReturnsAsync(expectedImprovedEssay);

            // when
            ImprovedEssay actualImprovedEssay =
                await this.improvedEssayService.ModifyEssayAsync(inpuImprovedEssay);

            // then
            actualImprovedEssay.Should().BeEquivalentTo(expectedImprovedEssay);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateImprovedEssayAsync(inpuImprovedEssay), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
