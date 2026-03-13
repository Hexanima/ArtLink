using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateProvidedServiceUseCaseTest
{
    [Fact]
    public async Task Should_Update_ProvidedService_When_It_Exists()
    {
        var providedServiceId = Guid.NewGuid();

        ProvidedService providedService = new ProvidedServiceMock(
            id: providedServiceId
        ).Create();

        var serviceMock = new Mock<IService<ProvidedService>>();
        serviceMock.Setup(s => s.GetById(providedServiceId))
                   .ReturnsAsync(new OperationResult<ProvidedService>(providedService));
        serviceMock.Setup(s => s.Update(providedService))
                   .ReturnsAsync(new OperationResult());

        var useCase = new UpdateProvidedServiceUseCase(serviceMock.Object);

        providedService.UserId = Guid.NewGuid();
        var result = await useCase.Execute(providedService);

        Assert.True(result.IsSuccess);
        serviceMock.Verify(s => s.Update(providedService), Times.Once);
    }
}
