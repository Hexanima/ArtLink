using Application.UseCases.Services;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetServiceUseCaseTest
{
    [Fact]
    public async Task Should_Return_Service_When_Found()
    {
        var serviceId = Guid.NewGuid();

        Service service = new ServiceMock(
            id: serviceId
        ).Create();

        var mockRepo = new Mock<IRepository<Service>>();
        mockRepo.Setup(r => r.GetById(serviceId))
                .ReturnsAsync(service);

        var serviceMock = new MockService<Service>(mockRepo.Object);
        var useCase = new GetServiceUseCase(serviceMock);

        var result = await useCase.Execute(serviceId);

        Assert.True(result.IsSuccess);
        Assert.Equal(serviceId, result.Value!.Id);
    }
}
