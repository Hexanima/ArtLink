using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetProvidedServiceUseCase
{
    private readonly IService<ProvidedService> _service;

    public GetProvidedServiceUseCase(IService<ProvidedService> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ProvidedService>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<ProvidedService>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ProvidedService>(new Exception("Provided service not found"));
        }

        return new OperationResult<ProvidedService>(result.Value);
    }
}
