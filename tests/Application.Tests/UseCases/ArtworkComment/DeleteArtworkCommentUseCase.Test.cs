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
    public async Task Should_Set_DeletedAt_When_ArtworkComment_Is_Deleted()
    {
        var artworkCommentId = Guid.NewGuid();

        ArtworkComment artworkComment = new ArtworkCommentMock(
            id: artworkCommentId
        ).Create();

        var mockRepo = new Mock<IRepository<ArtworkComment>>();
        mockRepo.Setup(r => r.GetById(artworkCommentId)).ReturnsAsync(artworkComment);
        mockRepo.Setup(r => r.Update(It.IsAny<ArtworkComment>(), artworkCommentId))
                .Callback<ArtworkComment, Guid>((updated, id) => artworkComment = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<ArtworkComment>(mockRepo.Object);
        var useCase = new DeleteArtworkCommentUseCase(service);

        await useCase.Execute(artworkCommentId);

        Assert.NotNull(artworkComment.DeletedAt);
        Assert.True(artworkComment.DeletedAt <= DateTime.UtcNow);
    }
}
