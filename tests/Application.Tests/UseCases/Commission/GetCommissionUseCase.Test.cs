using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetCommissionUseCaseTest
{
    [Fact]
    public async Task Should_Return_Commission_When_Found()
    {
        var commissionId = Guid.NewGuid();

        Commission commission = new CommissionMock(
            id: commissionId
        ).Create();

        var mockRepo = new Mock<IRepository<Commission>>();
        mockRepo.Setup(r => r.GetById(commissionId))
                .ReturnsAsync(commission);

        var service = new MockService<Commission>(mockRepo.Object);
        var useCase = new GetCommissionUseCase(service);

        var result = await useCase.Execute(commissionId);

        Assert.True(result.IsSuccess);
        Assert.Equal(commissionId, result.Value!.Id);
    }
}
