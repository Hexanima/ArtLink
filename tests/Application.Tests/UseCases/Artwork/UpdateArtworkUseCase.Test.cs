using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;
using Tests.Mocks.Entities;

namespace Test.Services;

public class UpdateArtworkUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_Artwork_Is_Updated()
    {
        var artworkId = Guid.NewGuid();

        Artwork artwork = new ArtworkMock(id:artworkId).Create();
       
        var mockRepo = new Mock<IRepository<Artwork>>();
        mockRepo.Setup(r => r.GetById(artworkId)).ReturnsAsync(artwork);
        mockRepo.Setup(r => r.Update(It.IsAny<Artwork>(), artworkId))
                .Callback<Artwork, Guid>((updated, id) => artwork = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<Artwork>(mockRepo.Object);
        var useCase = new UpdateArtworkUseCase(service);

        artwork.ArtworkName = "Updated Title";
        await useCase.Execute(artwork);

        Assert.Equal("Updated Title", artwork.ArtworkName);
        Assert.True(artwork.UpdatedAt > artwork.CreatedAt);

    }
}