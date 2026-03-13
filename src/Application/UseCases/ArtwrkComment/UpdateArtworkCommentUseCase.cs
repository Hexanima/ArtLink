using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateArtworkCommentUseCase
{
    private readonly IService<ArtworkComment> _service;

    public UpdateArtworkCommentUseCase(IService<ArtworkComment> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(ArtworkComment artworkComment)
    {
        if (artworkComment.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Artwork comment Id cannot be empty."));
        }

        OperationResult<ArtworkComment> existingArtworkCommentResult = await _service.GetById(artworkComment.Id);

        if (!existingArtworkCommentResult.IsSuccess)
        {
            return new OperationResult(new Exception("Artwork comment not found"));
        }

        artworkComment.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(artworkComment);        
    }
    
}