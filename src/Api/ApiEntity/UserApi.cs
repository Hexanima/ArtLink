namespace Api.ApiEntities;

public class UserApi
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public required string UserName { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public ICollection<ArtworkApi> ArtworkApis { get; set; } = new List<ArtworkApi>();
    public ICollection<ChatInviteApi> ChatInvites { get; set; } = new List<ChatInviteApi>();
    public ICollection<ChatUserApi> ChatUsers { get; set; } = new List<ChatUserApi>();
    public ICollection<ArtworkCommentApi> ArtworkComments { get; set; } = new List<ArtworkCommentApi>();
    public ICollection<ArtworkLikeApi> ArtworkLikes { get; set; } = new List<ArtworkLikeApi>();

    public ICollection<ProvidedServiceApi> ProvidedServices { get; set; } = new List<ProvidedServiceApi>();

    public required string HashedPassword { get; set; }
}
