using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteArtworkLikeUseCase
{
    private readonly IService<ArtworkLike> _service;

    public DeleteArtworkLikeUseCase(IService<ArtworkLike> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<ArtworkLike> existingArtworkLikeResult = await _service.GetById(id);

        if (!existingArtworkLikeResult.IsSuccess)
        {
            return new OperationResult(new Exception("Artwork like not found"));
        }

        return await _service.Delete(id);
    }
}
