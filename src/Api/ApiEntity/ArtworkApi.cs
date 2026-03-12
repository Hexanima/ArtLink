using System.Buffers.Text;
using Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace Api.ApiEntities;

public class ArtworkApi
{
    public Guid Id { get; set; }
    public bool OnSale { get; set; }
    public Guid UserId { get; set; }
    public User User{ get; set; }

    public Guid ServiceId { get; set; }
    public string ArtworkName{ get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
};


