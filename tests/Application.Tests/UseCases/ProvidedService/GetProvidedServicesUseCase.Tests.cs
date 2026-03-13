using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetProvidedServicesUseCaseTest
{
    [Fact]
    public async Task Should_Return_ProvidedServices_When_Found()
    {
        ProvidedService[] providedServices =
        {
            new ProvidedServiceMock().Create(),
            new ProvidedServiceMock().Create(),
            new ProvidedServiceMock().Create()
        };

        var serviceMock = new Mock<IService<ProvidedService>>();
        serviceMock.Setup(s => s.GetAll())
                   .ReturnsAsync(new OperationResult<ProvidedService[]>(providedServices));

        var useCase = new GetProvidedServicesUseCase(serviceMock.Object);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(providedServices.Length, result.Value!.Length);
    }
}
