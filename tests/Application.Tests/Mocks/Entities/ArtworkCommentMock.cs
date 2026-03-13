using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Domain.Types;

namespace Tests.Mocks.Entities;
public class ArtworkCommentMock
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ArtworkId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
public ArtworkCommentMock(
    Guid? id = null,
    Guid? userId = null,
    Guid? artworkId = null,
    string? message = null,
    DateTime? createdAt = null,
    DateTime? updatedAt = null,
    DateTime? deletedAt = null)
{
    Id = id ?? Guid.NewGuid();
    UserId = userId ?? Guid.NewGuid();
    ArtworkId = artworkId ?? Guid.NewGuid();
    Message = message ?? "Test message";
    CreatedAt = createdAt ?? DateTime.UtcNow;
    UpdatedAt = updatedAt ?? DateTime.UtcNow;
    DeletedAt = deletedAt;
}
    public ArtworkComment Create(){
    ArtworkComment result = new ArtworkComment
         {
            Id = Id ,
            UserId = UserId ,
            ArtworkId = ArtworkId ,
            Message = Message ,
            CreatedAt = CreatedAt ,
            UpdatedAt = UpdatedAt ,
            DeletedAt = DeletedAt 
        };
        return result;
        }

       
        
    }
    
