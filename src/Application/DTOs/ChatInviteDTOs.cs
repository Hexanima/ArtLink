namespace Application.DTOs;

public class CreateChatInviteDTO
{
    public required Guid ChatId { get; set; }
    public required Guid UserId { get; set; }
    public required Guid SentBy { get; set; }
}

public class UpdateChatInviteDTO
{
    public required Guid Id { get; set; }
    public Guid? ChatId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? SentBy { get; set; }
}

public class ChatInviteDTO
{
    public required Guid Id { get; set; }
    public required Guid ChatId { get; set; }
    public required Guid UserId { get; set; }
    public required Guid SentBy { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}