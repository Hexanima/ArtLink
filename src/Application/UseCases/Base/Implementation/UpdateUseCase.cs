using Domain.Services;
using Domain.Types;
using Application.UseCases.Base.Interfaces;

namespace Application.UseCases.Base.Implementation;

public class UpdateUseCase<T> : IUpdateUseCase<T> where T : IEntity
{
    private readonly IService<T> _service;

    public UpdateUseCase(IService<T> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(T entity)
    {
        if (entity.Id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        var existingEntityResult = await _service.GetById(entity.Id);
        if (!existingEntityResult.IsSuccess)
        {
            return new OperationResult(new Exception("Entity not found"));
        }

        return await _service.Update(entity);
    }
}
