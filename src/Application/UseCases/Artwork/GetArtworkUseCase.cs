using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetArtworkUseCase
{
    private readonly IService<Artwork> _service;

    public GetArtworkUseCase(IService<Artwork> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Artwork>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<Artwork>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Artwork>(new Exception("Artwork not found"));
        }

        return new OperationResult<Artwork>(result.Value);
    }
    
}