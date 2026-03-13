using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteArtworkLikeUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_ArtworkLike_Is_Deleted()
    {
        var artworkLikeId = Guid.NewGuid();

        ArtworkLike artworkLike = new ArtworkLikeMock(
            id: artworkLikeId
        ).Create();

        var mockRepo = new Mock<IRepository<ArtworkLike>>();
        mockRepo.Setup(r => r.GetById(artworkLikeId)).ReturnsAsync(artworkLike);
        mockRepo.Setup(r => r.Update(It.IsAny<ArtworkLike>(), artworkLikeId))
                .Callback<ArtworkLike, Guid>((updated, id) => artworkLike = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ArtworkLike>(mockRepo.Object);
        var useCase = new DeleteArtworkLikeUseCase(service);

        await useCase.Execute(artworkLikeId);

        Assert.NotNull(artworkLike.DeletedAt);
        Assert.True(artworkLike.DeletedAt <= DateTime.UtcNow);
    }
}
