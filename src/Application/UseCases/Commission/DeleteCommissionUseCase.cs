using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteCommissionUseCase
{
    private readonly IService<Commission> _service;

    public DeleteCommissionUseCase(IService<Commission> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<Commission> existingCommissionResult = await _service.GetById(id);

        if (!existingCommissionResult.IsSuccess)
        {
            return new OperationResult(new Exception("Commission not found"));
        }

        return await _service.Delete(id);
    }
}
