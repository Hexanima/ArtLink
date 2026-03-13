using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;
using Tests.Mocks.Entities;

namespace Test.Services;

public class GetArtworkCommentsUseCaseTest
{
    [Fact]
    public async Task Should_Return_ArtworkComments_When_Found()
    {
        ArtworkComment[] artworkComments =
        {
            new ArtworkCommentMock().Create(),
            new ArtworkCommentMock().Create(),
            new ArtworkCommentMock().Create()
        };

        var mockRepo = new Mock<IRepository<ArtworkComment>>();

        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(artworkComments.ToList());

        var service = new MockService<ArtworkComment>(mockRepo.Object);
        var useCase = new GetArtworkCommentsUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(artworkComments.Length, result.Value!.Length);
    }
}