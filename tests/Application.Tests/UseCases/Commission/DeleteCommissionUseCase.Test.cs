using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteCommissionUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_Commission_Is_Deleted()
    {
        var commissionId = Guid.NewGuid();

        Commission commission = new CommissionMock(
            id: commissionId
        ).Create();

        var mockRepo = new Mock<IRepository<Commission>>();
        mockRepo.Setup(r => r.GetById(commissionId)).ReturnsAsync(commission);
        mockRepo.Setup(r => r.Update(It.IsAny<Commission>(), commissionId))
                .Callback<Commission, Guid>((updated, id) => commission = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Commission>(mockRepo.Object);
        var useCase = new DeleteCommissionUseCase(service);

        await useCase.Execute(commissionId);

        Assert.NotNull(commission.DeletedAt);
        Assert.True(commission.DeletedAt <= DateTime.UtcNow);
    }
}
