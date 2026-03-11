using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MiApi.DTOs;
namespace MiApi.Controllers;

[ApiController]
[Route("api/Artwork")]
public class ArtworkController : ControllerBase
{
    readonly IFileService _service;

    public ArtworkController(IFileService service)
    {
        _service = service;
    }

    public async Task<IActionResult> GetPresignedImageUrl([FromBody] ArtworkDTO body)
    {
        
        
    }

    
}