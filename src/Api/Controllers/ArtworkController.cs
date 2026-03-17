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
public class ArtworkController : CrudController<Artwork, ArtworkDTO>
{
    private readonly CreateArtworkUseCase _create;

    public ArtworkController(
        IGetByIdUseCase<Artwork> getById,
         IDeleteUseCase<Artwork> delete,
        CreateArtworkUseCase create
    ) : base(getById, delete)
    {
        _create = create;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ArtworkDTO dto)
    {
        var userId = new Guid(User.FindFirst("id")?.Value ?? "");
        if (userId == Guid.Empty)
            return Unauthorized();

        var artwork = new CreateArtworkDTO{
            ArtworkName = dto.ArtworkName,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            OnSale = dto.OnSale,
            ServiceId = dto.ServiceId,
            UserId = userId
        };

        var result = await _create.Execute(artwork);

        if (!result.IsSuccess)
            return BadRequest(result.Error?.Message);

        return Ok();
    }
}