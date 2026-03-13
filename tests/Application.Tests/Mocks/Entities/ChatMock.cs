using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ChatMock
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string Title { get; set; }

    public ChatMock(
        Guid? id = null,
        string? title = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null)
    {
        Id = id ?? Guid.NewGuid();
        Title = title ?? "General chat";
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
    }

    public Chat Create()
    {
        Chat result = new Chat
        {
            Id = Id,
            Title = Title,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt
        };

        return result;
    }
}
