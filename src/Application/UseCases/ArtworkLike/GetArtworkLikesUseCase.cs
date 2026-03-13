using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetArtworkLikesUseCase
{
    private readonly IService<ArtworkLike> _service;

    public GetArtworkLikesUseCase(IService<ArtworkLike> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ArtworkLike[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ArtworkLike[]>(new Exception("No artwork likes found"));
        }

        return new OperationResult<ArtworkLike[]>(result.Value);
    }
}
