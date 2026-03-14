namespace Api.ApiEntities;

public class ChatApi
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<ChatMessageApi> Messages { get; set; } = new List<ChatMessageApi>();
    public ICollection<ChatInviteApi> Invites { get; set; } = new List<ChatInviteApi>();
    public ICollection<ChatUserApi> ChatUsers { get; set; } = new List<ChatUserApi>();

    public required string Title { get; set; }
}
