using aisha_ai.Brokers.Storages;
using aisha_ai.Models.ImprovedEssays;
using aisha_ai.Services.Foundations.ImprovedEssays;
using Moq;
using Tynamix.ObjectFiller;

namespace aisha_ai_unit_tests.Services.Foundations.ImprovedEssays
{
    public partial class ImprovedEssayTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IImprovedEssayService improvedEssayService;

        public ImprovedEssayTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.improvedEssayService =
                new ImprovedEssayService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static Filler<ImprovedEssay> CreateImprovedEssayFiller() =>
            new Filler<ImprovedEssay>();

        private static ImprovedEssay CreateRandomImprovedEssay() =>
            CreateImprovedEssayFiller().Create();
    }
}
