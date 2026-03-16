using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Domain.Types;

namespace Tests.Mocks.Entities;
public class ArtworkMock
{
            public Guid Id { get; set; }
            public string ArtworkName { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public Guid UserId { get; set; }
            public Guid ServiceId { get; set; }
            public bool OnSale { get; set; }
            public DateTime CreatedAt  { get; set; }
            public DateTime UpdatedAt  { get; set; }
            public DateTime? DeletedAt { get; set; }
public ArtworkMock(
    Guid? id = null,
    string? artworkName = null,
    string? description = null,
    string? imageUrl = null,
    Guid? userId = null,
    Guid? serviceId = null,
    bool? onSale = null,
    DateTime? createdAt = null,
    DateTime? updatedAt = null,
    DateTime? deletedAt = null)
{
    Id = id ?? Guid.NewGuid();
    ArtworkName = artworkName ?? "Test artwork";
    Description = description ?? "desc";
    ImageUrl = imageUrl ?? "url";
    UserId = userId ?? Guid.NewGuid();
    ServiceId = serviceId ?? Guid.NewGuid();
    OnSale = onSale ?? true;
    CreatedAt = createdAt ?? DateTime.UtcNow;
    UpdatedAt = updatedAt ?? DateTime.UtcNow;
    DeletedAt = deletedAt;
}
        public Artwork Create(){
        Artwork result = new Artwork(
            id: Id ,
            userId: UserId ,
            serviceId: ServiceId ,
            artworkName: ArtworkName ,
            description: Description,
            imageUrl: ImageUrl ,
            onSale: OnSale,
            createdAt: CreatedAt ,
            updatedAt: UpdatedAt ,
            deletedAt: DeletedAt 
        );
        return result;
        }

       
        
    }
    
