using System.ComponentModel.DataAnnotations;

namespace MiApi.DTOs;

public class ArtworkDTO
{
    public bool OnSale { get; set; }

    [Required]
    public string ArtworkName { get; set; }

    [Required]
    public string Description { get; set; }
}