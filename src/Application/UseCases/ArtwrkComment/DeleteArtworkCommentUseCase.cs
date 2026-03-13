using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteArtworkCommentUseCase
{
    private readonly IService<ArtworkComment> _service;

    public DeleteArtworkCommentUseCase(IService<ArtworkComment> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<ArtworkComment> existingArtworkCommentResult = await _service.GetById(id);

        if (!existingArtworkCommentResult.IsSuccess)
        {
            return new OperationResult(new Exception("Artwork comment not found"));
        }

        return await _service.Delete(id);        
    }
    
}