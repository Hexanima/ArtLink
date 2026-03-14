namespace Api.ApiEntities;

public class ArtworkApi
{
    public Guid Id { get; set; }
    public bool OnSale { get; set; }
    public Guid UserId { get; set; }
    public UserApi User { get; set; } = null!;

    public Guid ServiceId { get; set; }
    public ServiceApi Service { get; set; } = null!;
    public required string ArtworkName { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public ICollection<ArtworkCommentApi> ArtworkComments { get; set; } = new List<ArtworkCommentApi>();
    public ICollection<ArtworkLikeApi> ArtworkLikes { get; set; } = new List<ArtworkLikeApi>();

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}


