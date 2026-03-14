namespace Application.DTOs;

public class CreateChatUserDTO
{
    public required Guid UserId { get; set; }
    public required Guid ChatId { get; set; }
    public bool IsAdmin { get; set; } = false;
}

public class UpdateChatUserDTO
{
    public required Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ChatId { get; set; }
    public bool? IsAdmin { get; set; }
}

public class ChatUserDTO
{
    public required Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ChatId { get; set; }
    public required bool IsAdmin { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}