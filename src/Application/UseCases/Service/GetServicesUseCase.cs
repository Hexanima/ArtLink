using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Services;

public class GetServicesUseCase : IGetAllUseCase<Service>
{
    private readonly IService<Service> _service;

    public GetServicesUseCase(IService<Service> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Service[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Service[]>(new Exception("No services found"));
        }

        return new OperationResult<Service[]>(result.Value);
    }
}
