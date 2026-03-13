using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetCommissionUseCase
{
    private readonly IService<Commission> _service;

    public GetCommissionUseCase(IService<Commission> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Commission>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<Commission>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Commission>(new Exception("Commission not found"));
        }

        return new OperationResult<Commission>(result.Value);
    }
}
