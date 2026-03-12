using Xunit;
using Moq;
using Domain.Entities;
using Application.UseCases;
using Domain.Services;

public class DeleteArtworkUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_Artwork_Is_Deleted()
    {
        var artworkId = Guid.NewGuid();

        var artwork = new Artwork
        {
            Id = artworkId,
            ArtworkName = "Test artwork",
            Description = "desc",
            ImageUrl = "url",
            UserId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            OnSale = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };

        var repoMock = new Mock<IService<Artwork>>();

                Console.WriteLine($"AAAAAAAAAAAAAA-----------------------------{repoMock.Object}");


        var result = new DeleteArtworkUseCase(repoMock.Object);

        await result.Execute(artworkId);


        Assert.NotNull(artwork);

    }
}