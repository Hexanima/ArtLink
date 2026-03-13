using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetArtworksUseCase
{
    private readonly IService<Artwork> _service;

    public GetArtworksUseCase(IService<Artwork> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Artwork[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Artwork[]>(new Exception("No artworks found"));
        }

        return new OperationResult<Artwork[]>(result.Value);
    }
    
}