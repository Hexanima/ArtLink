using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetServicesUseCase
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
