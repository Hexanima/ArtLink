namespace Application.DTOs;

public class CreateChatDTO
{
    public required string Title { get; set; }
}

public class UpdateChatDTO
{
    public required Guid Id { get; set; }
    public string? Title { get; set; }
}

public class ChatDTO
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}