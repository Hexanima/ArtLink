using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetProvidedServicesUseCase
{
    private readonly IService<ProvidedService> _service;

    public GetProvidedServicesUseCase(IService<ProvidedService> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ProvidedService[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ProvidedService[]>(new Exception("No provided services found"));
        }

        return new OperationResult<ProvidedService[]>(result.Value);
    }
}
