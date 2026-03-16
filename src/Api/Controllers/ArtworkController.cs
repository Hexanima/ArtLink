using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using System.Threading.Tasks;
using Domain.Types;
using Domain.Services;
using Application.DTOs;
using Application.UseCases;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Api.Services;
namespace Api.Controllers;

[ApiController]
[Route("api/artworks")]
public class ArtworkController : ControllerBase
{
    private readonly IService<Artwork> _service;

   
    public ArtworkController(IService<Artwork> service)
    {
        _service = service;
    }

    
    [HttpGet]
    public IActionResult GetAllArtworks()
    {
        GetArtworksUseCase useCase = new GetArtworksUseCase(_service);
        OperationResult<Artwork[]> result = useCase.Execute().Result;
        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Error?.Message ?? "Error retrieving artworks" });
        }
        return Ok(result.Value);
    }
    

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOneArtwork(Guid id)
    {

    GetArtworkUseCase useCase = new GetArtworkUseCase(_service);
    OperationResult<Artwork> result = await useCase.Execute(id);
    if (!result.IsSuccess)
    {
        return NotFound(new {message=result.Error!.Message});
    }
    return Ok( result.Value );
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateArtwork([FromBody] ArtworkDTO artwork)
    {
        Guid userId = new Guid(User.FindFirst("id")?.Value ?? "");
        if (userId == Guid.Empty) return Unauthorized(new {message="User ID not found in token"});

        CreateArtworkUseCase useCase = new CreateArtworkUseCase(_service);
        OperationResult result = await useCase.Execute(new () {
            ArtworkName = artwork.ArtworkName, 
            Description = artwork.Description, 
            ImageUrl = artwork.ImageUrl,
            ServiceId = artwork.ServiceId,
            OnSale = artwork.OnSale,
            UserId = userId
        });
        
        if (!result.IsSuccess) return BadRequest(new {message=result.Error!.Message});
        
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public IActionResult DeleteArtwork(Guid id)
    {
        Guid userId = new Guid(User.FindFirst("id")?.Value ?? "");
        if (userId == Guid.Empty) return Unauthorized(new {message="User ID not found in token"});
        DeleteArtworkUseCase useCase = new DeleteArtworkUseCase(_service);
        OperationResult result = useCase.Execute(id).Result;
        if (!result.IsSuccess) return BadRequest(new {message=result.Error!.Message});
        
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateArtwork(Guid id, [FromBody] ArtworkDTO payload)
    {

        GetArtworkUseCase getArtworkUseCase = new GetArtworkUseCase(_service);
        OperationResult<Artwork> existingResult = await getArtworkUseCase.Execute(id);
        if (!existingResult.IsSuccess || existingResult.Value == null)
        {
            return NotFound(new { message = existingResult.Error?.Message ?? "Artwork not found" });
        }

         Artwork newArtwork = new Artwork(
            id: id,
            userId: payload.UserId,
            serviceId: payload.ServiceId ,
            artworkName: payload.ArtworkName ,
            description: payload.Description ,
            imageUrl: payload.ImageUrl ,
            onSale: payload.OnSale ,
            createdAt:DateTime.UtcNow,
            updatedAt:DateTime.UtcNow,
            deletedAt:null
                
        );
        {
            id = id;
        };

        UpdateArtworkUseCase useCase = new UpdateArtworkUseCase(_service);
        OperationResult result = await useCase.Execute(newArtwork);
        if (!result.IsSuccess) return BadRequest( new {message=result.Error!.Message} );
        
        return Ok(result.IsSuccess);
    }
    
}
