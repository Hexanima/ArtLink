using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateArtworkLikeUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_ArtworkLike_Is_Updated()
    {
        var artworkLikeId = Guid.NewGuid();

        ArtworkLike artworkLike = new ArtworkLikeMock(
            id: artworkLikeId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1)
        ).Create();

        var mockRepo = new Mock<IRepository<ArtworkLike>>();
        mockRepo.Setup(r => r.GetById(artworkLikeId)).ReturnsAsync(artworkLike);
        mockRepo.Setup(r => r.Update(It.IsAny<ArtworkLike>(), artworkLikeId))
                .Callback<ArtworkLike, Guid>((updated, id) => artworkLike = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ArtworkLike>(mockRepo.Object);
        var useCase = new UpdateArtworkLikeUseCase(service);

        await useCase.Execute(artworkLike);

        Assert.True(artworkLike.UpdatedAt > artworkLike.CreatedAt);
    }
}
