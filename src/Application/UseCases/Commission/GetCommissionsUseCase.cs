using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetCommissionsUseCase
{
    private readonly IService<Commission> _service;

    public GetCommissionsUseCase(IService<Commission> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Commission[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Commission[]>(new Exception("No commissions found"));
        }

        return new OperationResult<Commission[]>(result.Value);
    }
}
