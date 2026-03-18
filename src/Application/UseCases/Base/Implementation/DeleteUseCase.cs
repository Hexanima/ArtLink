using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Base.Implementation;

public class DeleteUseCase<T> : IDeleteUseCase<T> where T : IEntity
{
    private readonly IService<T> _service;

    public DeleteUseCase(IService<T> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<T> existingEntityResult = await _service.GetById(id);

        if (!existingEntityResult.IsSuccess)
        {
            return new OperationResult(new Exception("Entity not found"));
        }

        return await _service.Delete(id);        
    }
}
