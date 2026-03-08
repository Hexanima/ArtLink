using Domain.Types;

namespace Domain.Services;

public interface IService<T>
    where T : IEntity
{
    public Task<OperationResult<T>> GetById(Guid id);
    public Task<OperationResult<T[]>> GetAll();
    public Task<OperationResult> Create(T create);
    public Task<OperationResult> Update(T update);
    public Task<OperationResult> Delete(Guid id);
    public Task<OperationResult<T[]>> GetMany(Query query);
    public Task<OperationResult<T>> GetOne(Query query);
}
