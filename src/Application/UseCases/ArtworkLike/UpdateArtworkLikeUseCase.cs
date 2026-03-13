using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateArtworkLikeUseCase
{
    private readonly IService<ArtworkLike> _service;

    public UpdateArtworkLikeUseCase(IService<ArtworkLike> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(ArtworkLike artworkLike)
    {
        if (artworkLike.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Artwork like Id cannot be empty."));
        }

        OperationResult<ArtworkLike> existingArtworkLikeResult = await _service.GetById(artworkLike.Id);

        if (!existingArtworkLikeResult.IsSuccess)
        {
            return new OperationResult(new Exception("Artwork like not found"));
        }

        artworkLike.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(artworkLike);
    }
}
