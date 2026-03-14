namespace Api.ApiEntities;

public class ChatMessageApi
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid SentBy { get; set; }
    public Guid ChatId { get; set; }
    public ChatApi Chat { get; set; } = null!;

    public required string Message { get; set; }
}
