using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateCommissionUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_Commission_Is_Updated()
    {
        var commissionId = Guid.NewGuid();

        Commission commission = new CommissionMock(
            id: commissionId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1),
            description: "Original description"
        ).Create();

        var mockRepo = new Mock<IRepository<Commission>>();
        mockRepo.Setup(r => r.GetById(commissionId)).ReturnsAsync(commission);
        mockRepo.Setup(r => r.Update(It.IsAny<Commission>(), commissionId))
                .Callback<Commission, Guid>((updated, id) => commission = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Commission>(mockRepo.Object);
        var useCase = new UpdateCommissionUseCase(service);

        commission.Description = "Updated description";
        await useCase.Execute(commission);

        Assert.Equal("Updated description", commission.Description);
        Assert.True(commission.UpdatedAt > commission.CreatedAt);
    }
}
