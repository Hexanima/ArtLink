using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateArtworkUseCase
{
    private readonly IService<Artwork> _service;

    public UpdateArtworkUseCase(IService<Artwork> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Artwork artwork)
    {
        if (artwork.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Artwork Id cannot be empty."));
        }

        OperationResult<Artwork> existingArtworkResult = await _service.GetById(artwork.Id);

        if (!existingArtworkResult.IsSuccess)
        {
            return new OperationResult(new Exception("Artwork not found"));
        }

        artwork.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(artwork);        
    }
    
}