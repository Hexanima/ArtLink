using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetArtworkCommentUseCase
{
    private readonly IService<ArtworkComment> _service;

    public GetArtworkCommentUseCase(IService<ArtworkComment> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ArtworkComment>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<ArtworkComment>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ArtworkComment>(new Exception("Artwork comment not found"));
        }

        return new OperationResult<ArtworkComment>(result.Value);
    }
    
}