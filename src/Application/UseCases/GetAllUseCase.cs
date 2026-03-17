using Domain.Services;
using Domain.Types;

public class GetAllUseCase<T> where T : IEntity
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