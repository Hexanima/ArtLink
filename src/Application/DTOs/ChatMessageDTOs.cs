namespace Application.DTOs;

public class CreateChatMessageDTO
{
    public required Guid SentBy { get; set; }
    public required Guid ChatId { get; set; }
    public required string Message { get; set; }
}

public class UpdateChatMessageDTO
{
    public required Guid Id { get; set; }
    public Guid? SentBy { get; set; }
    public Guid? ChatId { get; set; }
    public string? Message { get; set; }
}

public class ChatMessageDTO
{
    public required Guid Id { get; set; }
    public required Guid SentBy { get; set; }
    public required Guid ChatId { get; set; }
    public required string Message { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}