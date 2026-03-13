using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetArtworkLikeUseCaseTest
{
    [Fact]
    public async Task Should_Return_ArtworkLike_When_Found()
    {
        var artworkLikeId = Guid.NewGuid();

        ArtworkLike artworkLike = new ArtworkLikeMock(
            id: artworkLikeId
        ).Create();

        var mockRepo = new Mock<IRepository<ArtworkLike>>();
        mockRepo.Setup(r => r.GetById(artworkLikeId))
                .ReturnsAsync(artworkLike);

        var service = new MockService<ArtworkLike>(mockRepo.Object);
        var useCase = new GetArtworkLikeUseCase(service);

        var result = await useCase.Execute(artworkLikeId);

        Assert.True(result.IsSuccess);
        Assert.Equal(artworkLikeId, result.Value!.Id);
    }
}
