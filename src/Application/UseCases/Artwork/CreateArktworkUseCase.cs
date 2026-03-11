using Domain.Entities;
using Domain.Types;
using Domain.Services;

namespace Application.UseCase.ArtWork;

public class CreateArktworkUseCase
{
    private readonly IService<Artwork> _service;

    public CreateArktworkUseCase(IService service)
    {
        _service = service;
    }


    public Task Execute(ArtWorkDTOs artwork, Guid id)
    {
        Artwork newArtwork = new Artwork
        {
        artwork.OnSale,


    artwork.ArtworkName,


    artwork.Description,
            
        };

        await _service.create(newArtwork);
    } 

}