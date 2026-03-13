using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;
using Tests.Mocks.Entities;

namespace Test.Services;

public class GetArtworkCommentsUseCaseTest
{
    [Fact]
    public async Task Should_Return_Artwork_When_Found()
    {
        Artwork[] artworks=
        {
            new ArtworkMock().Create(),
            new ArtworkMock().Create(),
            new ArtworkMock().Create()
        };

        var mockRepo = new Mock<IRepository<Artwork>>();

        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(artworks.ToList());
        

        var service = new MockService<Artwork>(mockRepo.Object);
        var useCase = new GetArtworksUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(artworks.Length, result.Value!.Length);
    }
}