using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetArtworkLikeUseCase
{
    private readonly IService<ArtworkLike> _service;

    public GetArtworkLikeUseCase(IService<ArtworkLike> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ArtworkLike>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<ArtworkLike>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ArtworkLike>(new Exception("Artwork like not found"));
        }

        return new OperationResult<ArtworkLike>(result.Value);
    }
}
