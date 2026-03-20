using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Types;
using Application.DTOs;
using Application.UseCases.Base.Interfaces;
using Api.Controllers.Base;
using Api.Mappers;

namespace Api.Controllers;

[ApiController]
[Route("api/services")]
public class ServiceController : CrudController<Service, ServiceDTO>
{
    public ServiceController(
        IGetAllUseCase<Service> getAll,
        IGetByIdUseCase<Service> getById,
        ICreateUseCase<Service> create,
        IUpdateUseCase<Service> update,
        IDeleteUseCase<Service> delete,
        IMapper<ServiceDTO, Service> mapper
    ) : base(getAll, getById, create, update, delete, mapper)
    {
    }
}