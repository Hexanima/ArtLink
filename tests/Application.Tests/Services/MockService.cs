using Domain.Types;
using Microsoft.EntityFrameworkCore;
using Domain.Services;
using Domain.Entities;

namespace Test.Services;

public class MockService<T> : IService<T> where T : class, IEntity ,ITimestampedEntity,ISoftDeletedEntity
{
    private readonly IRepository<T> _repository;

    public MockService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult<T>> GetById(Guid id)
    {
        T entity = await _repository.GetById(id);

            if(entity == null) return new OperationResult<T>(new Exception("not found"));
            
        return new OperationResult<T>(entity);
    }

    public async Task<OperationResult<T[]>> GetAll()
    {
        var list = await _repository.GetAll();
        T[] ListArray = list.ToArray();
        return new OperationResult<T[]>(ListArray);
    }

    public async Task<OperationResult> Create(T create)
    {
        await _repository.Add(create);
        return new OperationResult();
    }

    public async Task<OperationResult> Update(T update)
    {
        var existing = await _repository.GetById(update.Id);
        if (existing == null)
            return new OperationResult(new Exception("Fail"));

        await _repository.Update(update, update.Id);
        return new OperationResult();
    }

    public async Task<OperationResult> Delete(Guid id)
    {
        var entity = await _repository.GetById(id);
        if (entity == null)
            return new OperationResult(new Exception("Fail"));

            entity.DeletedAt = DateTime.UtcNow;        
        return new OperationResult();
    }

    public Task<OperationResult<T[]>> GetMany(Query query)
    {
        // Aquí puedes implementar lógica de filtrado según tu Query
        throw new NotImplementedException();
    }

    public Task<OperationResult<T>> GetOne(Query query)
    {
        // Similar a GetMany pero retorna solo uno
        throw new NotImplementedException();
    }
}