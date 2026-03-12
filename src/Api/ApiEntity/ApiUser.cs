using Domain.Types;
namespace Api.ApiEntities;

public class User : IEntity, ISoftDeletedEntity, ITimestampedEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required string UserName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public ICollection<ArtworkApi> ArtworkApis { get; set; }
    public required string HashedPassword { get; set; }

}
