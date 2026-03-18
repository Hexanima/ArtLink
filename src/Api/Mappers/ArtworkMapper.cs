using Application.DTOs;
using Domain.Entities;

namespace Api.Mappers;

public class ArtworkMapper : IMapper<ArtworkDTO, Artwork>
{
    public Artwork Map(ArtworkDTO dto)
    {
        return new Artwork(
            id: Guid.NewGuid(),
            userId: dto.UserId,
            serviceId: dto.ServiceId,
            artworkName: dto.ArtworkName,
            description: dto.Description,
            imageUrl: dto.ImageUrl,
            onSale: dto.OnSale,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow,
            deletedAt: null
        );
    }
}
