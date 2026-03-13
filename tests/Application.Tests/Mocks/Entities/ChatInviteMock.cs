using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ChatInviteMock
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public Guid SentBy { get; set; }

    public ChatInviteMock(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        Guid? chatId = null,
        Guid? userId = null,
        Guid? sentBy = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
        ChatId = chatId ?? Guid.NewGuid();
        UserId = userId ?? Guid.NewGuid();
        SentBy = sentBy ?? Guid.NewGuid();
    }

    public ChatInvite Create()
    {
        ChatInvite result = new ChatInvite
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt,
            ChatId = ChatId,
            UserId = UserId,
            SentBy = SentBy
        };

        return result;
    }
}
