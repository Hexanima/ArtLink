using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetCommissionsUseCaseTest
{
    [Fact]
    public async Task Should_Return_Commissions_When_Found()
    {
        Commission[] commissions =
        {
            new CommissionMock().Create(),
            new CommissionMock().Create(),
            new CommissionMock().Create()
        };

        var mockRepo = new Mock<IRepository<Commission>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(commissions.ToList());

        var service = new MockService<Commission>(mockRepo.Object);
        var useCase = new GetCommissionsUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(commissions.Length, result.Value!.Length);
    }
}
