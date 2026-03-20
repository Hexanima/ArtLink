using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Services;

public class UpdateServiceUseCase : IUpdateUseCase<Service>
{
    private readonly IService<Service> _service;

    public UpdateServiceUseCase(IService<Service> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Service service)
    {
        if (service.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Service Id cannot be empty."));
        }

        OperationResult<Service> existingServiceResult = await _service.GetById(service.Id);

        if (!existingServiceResult.IsSuccess)
        {
            return new OperationResult(new Exception("Service not found"));
        }

        service.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(service);
    }
}
