using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Types;
using Application.DTOs;
using Application.UseCases.Base.Interfaces;
using Api.Controllers.Base;
using Api.Mappers;

namespace Api.Controllers;

[ApiController]
[Route("api/artworks")]
public class ArtworkController : CrudController<Artwork, ArtworkDTO>
{
    public ArtworkController(
        IGetAllUseCase<Artwork> getAll,
        IGetByIdUseCase<Artwork> getById,
        ICreateUseCase<Artwork> create,
        IUpdateUseCase<Artwork> update,
        IDeleteUseCase<Artwork> delete,
        IMapper<ArtworkDTO, Artwork> mapper
    ) : base(getAll, getById, create, update, delete, mapper)
    {
    }
}