using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;
using Tests.Mocks.Entities;

namespace Test.Services;

public class DeleteArtworkCommentUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_Artwork_Is_Deleted()
    {
        var artworkId = Guid.NewGuid();

        ArtworkComment artworkComment = new ArtworkCommentMock(
            id: artworkId
        ).Create();
       

        var mockRepo = new Mock<IRepository<ArtworkComment>>();
        mockRepo.Setup(r => r.GetById(artworkId)).ReturnsAsync(artworkComment);
        mockRepo.Setup(r => r.Update(It.IsAny<ArtworkComment>(), artworkId))
                .Callback<ArtworkComment, Guid>((updated, id) => artworkComment = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ArtworkComment>(mockRepo.Object);
        var useCase = new DeleteArtworkCommentUseCase(service);

        await useCase.Execute(artworkId);

        Assert.NotNull(artworkComment.DeletedAt);
        Assert.True(artworkComment.DeletedAt <= DateTime.UtcNow);
    }
    //     public async Task Should_Throw_Exception_When_Artwork_Is_Not_Found()
    // {
    //     var artworkId = Guid.NewGuid();

    //     ArtworkComment artworkComment = new ArtworkCommentMock(
    //     ).Create();
       

    //     var mockRepo = new Mock<IRepository<ArtworkComment>>();
    //     mockRepo.Setup(r => r.GetById(artworkId)).ReturnsAsync(artworkComment);
    //     mockRepo.Setup(r => r.Update(It.IsAny<ArtworkComment>(), artworkId))
    //             .Callback<ArtworkComment, Guid>((updated, id) => artworkComment = updated)
    //             .Returns(Task.CompletedTask);

    //     var service = new MockService<ArtworkComment>(mockRepo.Object);
    //     var useCase = new DeleteArtworkCommentUseCase(service);

    //     await useCase.Execute(artworkId);

    //     Assert.IsType<Exception>(artwork);
    // }
}