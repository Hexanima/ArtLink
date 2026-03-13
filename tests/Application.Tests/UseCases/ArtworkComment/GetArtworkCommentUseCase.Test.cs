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
    public async Task Should_Return_ArtworkComment_When_Found()
    {
        var artworkCommentId = Guid.NewGuid();

        ArtworkComment artworkComment = new ArtworkCommentMock(
            id: artworkCommentId
        ).Create();

        var mockRepo = new Mock<IRepository<ArtworkComment>>();
        mockRepo.Setup(r => r.GetById(artworkCommentId))
                .ReturnsAsync(artworkComment);

        var service = new MockService<ArtworkComment>(mockRepo.Object);
        var useCase = new GetArtworkCommentUseCase(service);

        var result = await useCase.Execute(artworkCommentId);

        Assert.True(result.IsSuccess);
        Assert.Equal(artworkCommentId, result.Value!.Id);
    }
}