using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetArtworkCommentsUseCase
{
    private readonly IService<ArtworkComment> _service;

    public GetArtworkCommentsUseCase(IService<ArtworkComment> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ArtworkComment[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ArtworkComment[]>(new Exception("No artwork comments found"));
        }

        return new OperationResult<ArtworkComment[]>(result.Value);
    }
    
}