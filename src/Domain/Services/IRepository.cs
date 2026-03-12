namespace Domain.Services;
public interface IRepository<T> where T : class
{
    Task Add (T entity);
    Task <T>? GetById(Guid id);
    Task <List<T>> GetAll();
    Task Remove(Guid id);
    Task Update(T Entity, Guid id);
}