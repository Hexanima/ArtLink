using Application.UseCases.Services;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetServicesUseCaseTest
{
    [Fact]
    public async Task Should_Return_Services_When_Found()
    {
        Service[] services =
        {
            new ServiceMock().Create(),
            new ServiceMock().Create(),
            new ServiceMock().Create()
        };

        var mockRepo = new Mock<IRepository<Service>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(services.ToList());

        var serviceMock = new MockService<Service>(mockRepo.Object);
        var useCase = new GetServicesUseCase(serviceMock);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(services.Length, result.Value!.Length);
    }
}
