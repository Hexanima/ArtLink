using Domain.Services;
using Domain.Types;

public class GetByIdUseCase<T>where T : IEntity
{
    private readonly IService<T> _service;

    public GetByIdUseCase(IService<T> service)
    {
        _service = service;
    }

    public async Task<OperationResult<T>> Execute(Guid id)
    {
        var entity = await _service.GetById(id);

     
        if (!entity.IsSuccess || entity.Value == null)
        {
            return new OperationResult<T>(new Exception("Entity not found"));
        }

        return new OperationResult<T>(entity.Value);
    }
}