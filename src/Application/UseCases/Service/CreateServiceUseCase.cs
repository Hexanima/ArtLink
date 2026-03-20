using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Services;

public class CreateServiceUseCase : ICreateUseCase<Service>
{
    private readonly IService<Service> _service;

    public CreateServiceUseCase(IService<Service> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Service entity)
    {
        return await _service.Create(entity);
    }
}