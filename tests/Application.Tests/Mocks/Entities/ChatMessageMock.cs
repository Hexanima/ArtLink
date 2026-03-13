using Domain.Entities;

namespace Tests.Mocks.Entities;
public class ChatMessageMock
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid SentBy { get; set; }
    public Guid ChatId { get; set; }
    public string Message { get; set; }

    public ChatMessageMock(
        Guid? id = null,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        Guid? sentBy = null,
        Guid? chatId = null,
        string? message = null)
    {
        Id = id ?? Guid.NewGuid();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt ?? DateTime.UtcNow;
        DeletedAt = deletedAt;
        SentBy = sentBy ?? Guid.NewGuid();
        ChatId = chatId ?? Guid.NewGuid();
        Message = message ?? "Test message";
    }

    public ChatMessage Create()
    {
        ChatMessage result = new ChatMessage
        {
            Id = Id,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt,
            DeletedAt = DeletedAt,
            SentBy = SentBy,
            ChatId = ChatId,
            Message = Message
        };

        return result;
    }
}
