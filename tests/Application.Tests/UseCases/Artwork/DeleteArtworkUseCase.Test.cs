using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;
using Tests.Mocks.Entities;

namespace Test.Services;

public class DeleteArtworkUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_Artwork_Is_Deleted()
    {
        var artworkId = Guid.NewGuid();

        Artwork artwork = new ArtworkMock(
            id: artworkId
        ).Create();
       

        var mockRepo = new Mock<IRepository<Artwork>>();
        mockRepo.Setup(r => r.GetById(artworkId)).ReturnsAsync(artwork);
        mockRepo.Setup(r => r.Update(It.IsAny<Artwork>(), artworkId))
                .Callback<Artwork, Guid>((updated, id) => artwork = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Artwork>(mockRepo.Object);
        var useCase = new DeleteArtworkUseCase(service);

        await useCase.Execute(artworkId);

        Assert.NotNull(artwork.DeletedAt);
        Assert.True(artwork.DeletedAt <= DateTime.UtcNow);
    }
        public async Task Should_Throw_Exception_When_Artwork_Is_Not_Found()
    {
        var artworkId = Guid.NewGuid();

        Artwork artwork = new ArtworkMock(
        ).Create();
       

        var mockRepo = new Mock<IRepository<Artwork>>();
        mockRepo.Setup(r => r.GetById(artworkId)).ReturnsAsync(artwork);
        mockRepo.Setup(r => r.Update(It.IsAny<Artwork>(), artworkId))
                .Callback<Artwork, Guid>((updated, id) => artwork = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Artwork>(mockRepo.Object);
        var useCase = new DeleteArtworkUseCase(service);

        await useCase.Execute(artworkId);

        Assert.IsType<Exception>(artwork);
    }
}