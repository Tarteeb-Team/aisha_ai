using System.Linq;
using aisha_ai.Brokers.Storages;
using aisha_ai.Models.EssayModels.Essays;
using aisha_ai.Services.Foundations.Essays;
using Moq;
using Tynamix.ObjectFiller;

namespace aisha_ai_unit_tests.Services.Foundations.Essays;

public partial class EssayServiceTests
{
    private readonly Mock<IStorageBroker> storageBrokerMock;
    private readonly IEssayService essayService;

    public EssayServiceTests()
    {
        this.storageBrokerMock = new Mock<IStorageBroker>();
        this.essayService = new EssayService(
            storageBroker: this.storageBrokerMock.Object);
    }

    private static int GetRandomNumber() =>
        new IntRange(min: 2, max: 10).GetValue();

    private static Filler<Essay> CreateEssayFiller() =>
        new Filler<Essay>();

    private static Essay CreateRandomEssay() =>
        CreateEssayFiller().Create();

    private static IQueryable<Essay> CreateRandomEssays() =>
        CreateEssayFiller().Create(count: GetRandomNumber()).AsQueryable();
}