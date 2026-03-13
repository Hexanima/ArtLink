using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteProvidedServiceUseCase
{
    private readonly IService<ProvidedService> _service;

    public DeleteProvidedServiceUseCase(IService<ProvidedService> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<ProvidedService> existingProvidedServiceResult = await _service.GetById(id);

        if (!existingProvidedServiceResult.IsSuccess)
        {
            return new OperationResult(new Exception("Provided service not found"));
        }

        return await _service.Delete(id);
    }
}
