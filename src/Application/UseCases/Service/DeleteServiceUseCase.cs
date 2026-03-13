using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteServiceUseCase
{
    private readonly IService<Service> _service;

    public DeleteServiceUseCase(IService<Service> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<Service> existingServiceResult = await _service.GetById(id);

        if (!existingServiceResult.IsSuccess)
        {
            return new OperationResult(new Exception("Service not found"));
        }

        return await _service.Delete(id);
    }
}
