using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Base.Implementation;

public class GetAllUseCase<T> : IGetAllUseCase<T> where T : IEntity
{
    private readonly IService<T> _service;

    public GetAllUseCase(IService<T> service)
    {
        _service = service;
    }

    public async Task<OperationResult<T[]>> Execute()
    {
        var result = await _service.GetAll();
        if (!result.IsSuccess)
        {
            return new OperationResult<T[]>(result.Error);
        }
        return new OperationResult<T[]>(result.Value);
    }
}
