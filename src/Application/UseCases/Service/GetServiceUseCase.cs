using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Services;

public class GetServiceUseCase : IGetByIdUseCase<Service>
{
    private readonly IService<Service> _service;

    public GetServiceUseCase(IService<Service> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Service>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<Service>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Service>(new Exception("Service not found"));
        }

        return new OperationResult<Service>(result.Value);
    }
}
