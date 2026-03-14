namespace Api.ApiEntities;
public class ChatInviteApi
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Guid ChatId { get; set; }
    public Guid UserId { get; set; }
    public Guid SentBy { get; set; }
    public ChatApi Chat { get; set; } = null!;
    public UserApi User { get; set; } = null!;
}
