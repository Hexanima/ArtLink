using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetArtworkLikesUseCaseTest
{
    [Fact]
    public async Task Should_Return_ArtworkLikes_When_Found()
    {
        ArtworkLike[] artworkLikes =
        {
            new ArtworkLikeMock().Create(),
            new ArtworkLikeMock().Create(),
            new ArtworkLikeMock().Create()
        };

        var mockRepo = new Mock<IRepository<ArtworkLike>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(artworkLikes.ToList());

        var service = new MockService<ArtworkLike>(mockRepo.Object);
        var useCase = new GetArtworkLikesUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(artworkLikes.Length, result.Value!.Length);
    }
}
