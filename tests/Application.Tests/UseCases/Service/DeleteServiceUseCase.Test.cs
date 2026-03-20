using Application.UseCases.Services;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteServiceUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_Service_Is_Deleted()
    {
        var serviceId = Guid.NewGuid();

        Service service = new ServiceMock(
            id: serviceId
        ).Create();

        var mockRepo = new Mock<IRepository<Service>>();
        mockRepo.Setup(r => r.GetById(serviceId)).ReturnsAsync(service);
        mockRepo.Setup(r => r.Update(It.IsAny<Service>(), serviceId))
                .Callback<Service, Guid>((updated, id) => service = updated)
                .Returns(Task.CompletedTask);

        var serviceMock = new MockService<Service>(mockRepo.Object);
        var useCase = new DeleteServiceUseCase(serviceMock);

        await useCase.Execute(serviceId);

        Assert.NotNull(service.DeletedAt);
        Assert.True(service.DeletedAt <= DateTime.UtcNow);
    }
}
