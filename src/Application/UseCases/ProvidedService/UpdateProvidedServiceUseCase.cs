using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateProvidedServiceUseCase
{
    private readonly IService<ProvidedService> _service;

    public UpdateProvidedServiceUseCase(IService<ProvidedService> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(ProvidedService providedService)
    {
        if (providedService.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Provided service Id cannot be empty."));
        }

        OperationResult<ProvidedService> existingProvidedServiceResult = await _service.GetById(providedService.Id);

        if (!existingProvidedServiceResult.IsSuccess)
        {
            return new OperationResult(new Exception("Provided service not found"));
        }

        return await _service.Update(providedService);
    }
}
