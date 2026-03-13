using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class UpdateCommissionUseCase
{
    private readonly IService<Commission> _service;

    public UpdateCommissionUseCase(IService<Commission> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Commission commission)
    {
        if (commission.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Commission Id cannot be empty."));
        }

        OperationResult<Commission> existingCommissionResult = await _service.GetById(commission.Id);

        if (!existingCommissionResult.IsSuccess)
        {
            return new OperationResult(new Exception("Commission not found"));
        }

        commission.UpdatedAt = DateTime.UtcNow;
        return await _service.Update(commission);
    }
}
