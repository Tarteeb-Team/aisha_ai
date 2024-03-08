using System.Linq;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.ImprovedEssays;
using aisha_ai.Services.EssayServices.Foundations.ImprovedEssays;
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

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 9).GetValue();

        private static Filler<ImprovedEssay> CreateImprovedEssayFiller() =>
            new Filler<ImprovedEssay>();

        private static ImprovedEssay CreateRandomImprovedEssay() =>
            CreateImprovedEssayFiller().Create();

        private IQueryable<ImprovedEssay> CreateRandomImprovedEssays() =>
            CreateImprovedEssayFiller().Create(count: GetRandomNumber()).AsQueryable();
    }
}
