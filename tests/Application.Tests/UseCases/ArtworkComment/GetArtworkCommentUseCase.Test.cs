using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;
using Tests.Mocks.Entities;

namespace Test.Services;

public class GetArtworkCommentUseCaseTest
{
    [Fact]
    public async Task Should_Return_Artwork_When_Found()
    {
        var artworkId = Guid.NewGuid();

        Artwork artwork = new ArtworkMock(
            id: artworkId
        ).Create();
       

var mockRepo = new Mock<IRepository<Artwork>>();
    mockRepo.Setup(r => r.GetById(artworkId))
            .ReturnsAsync(artwork);

        var service = new MockService<Artwork>(mockRepo.Object);
        var useCase = new GetArtworkUseCase(service);

        var result = await useCase.Execute(artworkId);

        Assert.True(result.IsSuccess);
        Assert.Equal(artworkId, result.Value!.Id);
    }
}