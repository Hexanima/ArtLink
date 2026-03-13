using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetProvidedServiceUseCaseTest
{
    [Fact]
    public async Task Should_Return_ProvidedService_When_Found()
    {
        var providedServiceId = Guid.NewGuid();

        ProvidedService providedService = new ProvidedServiceMock(
            id: providedServiceId
        ).Create();

        var serviceMock = new Mock<IService<ProvidedService>>();
        serviceMock.Setup(s => s.GetById(providedServiceId))
                   .ReturnsAsync(new OperationResult<ProvidedService>(providedService));

        var useCase = new GetProvidedServiceUseCase(serviceMock.Object);

        var result = await useCase.Execute(providedServiceId);

        Assert.True(result.IsSuccess);
        Assert.Equal(providedServiceId, result.Value!.Id);
    }
}
