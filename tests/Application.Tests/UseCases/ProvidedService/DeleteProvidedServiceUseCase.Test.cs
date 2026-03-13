using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteProvidedServiceUseCaseTest
{
    [Fact]
    public async Task Should_Delete_ProvidedService_When_It_Exists()
    {
        var providedServiceId = Guid.NewGuid();

        ProvidedService providedService = new ProvidedServiceMock(
            id: providedServiceId
        ).Create();

        var serviceMock = new Mock<IService<ProvidedService>>();
        serviceMock.Setup(s => s.GetById(providedServiceId))
                   .ReturnsAsync(new OperationResult<ProvidedService>(providedService));
        serviceMock.Setup(s => s.Delete(providedServiceId))
                   .ReturnsAsync(new OperationResult());

        var useCase = new DeleteProvidedServiceUseCase(serviceMock.Object);

        var result = await useCase.Execute(providedServiceId);

        Assert.True(result.IsSuccess);
        serviceMock.Verify(s => s.Delete(providedServiceId), Times.Once);
    }
}
