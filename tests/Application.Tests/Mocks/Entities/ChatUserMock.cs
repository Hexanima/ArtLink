using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ChatUserMock
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public bool IsAdmin { get; set; }

    public ChatUserMock(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        Guid? userId = null,
        Guid? chatId = null,
        bool? isAdmin = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
        UserId = userId ?? Guid.NewGuid();
        ChatId = chatId ?? Guid.NewGuid();
        IsAdmin = isAdmin ?? false;
    }

    public ChatUser Create()
    {
        ChatUser result = new ChatUser
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt,
            UserId = UserId,
            ChatId = ChatId,
            IsAdmin = IsAdmin
        };

        return result;
    }
}
