using Application.DTOs;
using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class CreateArtworkUseCase
{
    private readonly IService<Artwork> _service;

    public CreateArtworkUseCase(IService<Artwork> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(CreateArtworkDTO create)
    {
        var newArtwork = new Artwork(
            id: Guid.NewGuid(),
            userId: create.UserId,
            serviceId: create.ServiceId ,
            artworkName: create.ArtworkName ,
            description: create.Description ,
            imageUrl: create.ImageUrl ,
            onSale: create.OnSale ,
            createdAt:DateTime.UtcNow,
            updatedAt:DateTime.UtcNow,
            deletedAt:null
                
        );
        
        return await _service.Create(newArtwork);
    }
    
}