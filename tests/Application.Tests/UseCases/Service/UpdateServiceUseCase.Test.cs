using Application.UseCases.Services;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateServiceUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_Service_Is_Updated()
    {
        var serviceId = Guid.NewGuid();

        Service service = new ServiceMock(
            id: serviceId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1),
            serviceName: "Original name"
        ).Create();

        var mockRepo = new Mock<IRepository<Service>>();
        mockRepo.Setup(r => r.GetById(serviceId)).ReturnsAsync(service);
        mockRepo.Setup(r => r.Update(It.IsAny<Service>(), serviceId))
                .Callback<Service, Guid>((updated, id) => service = updated)
                .Returns(Task.CompletedTask);

        var serviceMock = new MockService<Service>(mockRepo.Object);
        var useCase = new UpdateServiceUseCase(serviceMock);

        service.ServiceName = "Updated name";
        await useCase.Execute(service);

        Assert.Equal("Updated name", service.ServiceName);
        Assert.True(service.UpdatedAt > service.CreatedAt);
    }
}
