using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Services;

public class PostgreService<T> : IService<T> where T :class, IEntity
{
    private readonly AppDbContext _ctx;
    private readonly DbSet<T> _dbSet;
    public PostgreService(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = ctx.Set<T>();
    }
    
    public async Task<OperationResult> Create(T create)
    {
        try {
            await _dbSet.AddAsync(create);
            await _ctx.SaveChangesAsync();
            return new OperationResult();
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }

    public async Task<OperationResult> Delete(Guid id)
    {
        try {
        var entity = await _dbSet.FindAsync(id);
            if (entity == null) return new OperationResult(new Exception("Not found"));
        _dbSet.Remove(entity);
        await _ctx.SaveChangesAsync();
        
        return new OperationResult();
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }

    public async Task<OperationResult<T[]>> GetAll()
    {
        try {
        var items = await _dbSet.ToArrayAsync();
        
        return new OperationResult<T[]>(items);

        }
        catch (Exception ex)
        {
            return new OperationResult<T[]>(ex);
        }
    }

    public async Task<OperationResult<T>> GetById(Guid id)
    {
        var item = await _dbSet
            .FirstOrDefaultAsync(u => u.Id == id);
            if (item == null) return new OperationResult<T>(new Exception("Not found"));
            return new OperationResult<T>(item);
    }

    public async Task<OperationResult> Update(T update)
    {
        try
        {
            var existing = await _dbSet.FindAsync(update.Id);
            if (existing == null) return new OperationResult(new Exception("Not found"));
            _dbSet.Update(update);
            await _ctx.SaveChangesAsync();
            return new OperationResult();
        } catch (Exception e)
        {
            return new OperationResult(e);
        }
    }

    public async Task<OperationResult<T[]>> GetMany(Query query)
    {
        try
        {
            IQueryable<T> users = _dbSet.AsQueryable();

            if (query.Filters != null)
            {
                users = ApplyFilter(users, query.Filters);
            }

            if (query.Offset.HasValue)
                users = users.Skip(query.Offset.Value);

            if (query.Limit.HasValue)
                users = users.Take(query.Limit.Value);

            var result = await users.ToArrayAsync();
            return new OperationResult<T[]>(result);
        }
        catch (Exception ex)
        {
            return new OperationResult<T[]>(ex);
        }
    }

    public async Task<OperationResult<T>> GetOne(Query query)
    {
        try
        {
            IQueryable<T> items = _dbSet.AsQueryable();

            if (query.Filters != null)
            {
                items = ApplyFilter(items, query.Filters);
            }

            var item = await items.FirstOrDefaultAsync();
            if (item == null)
                return new OperationResult<T>(new Exception("Not found"));

            return new OperationResult<T>(item);
        }
        catch (Exception ex)
        {
            return new OperationResult<T>(ex);
        }
    }

    // Aplica los filtros recursivamente
    private IQueryable<T> ApplyFilter(IQueryable<T> query, IFilter filter)
    {
        if (filter is BaseFilter baseFilter)
        {
            var param = Expression.Parameter(typeof(T), "x");
            var member = Expression.PropertyOrField(param, baseFilter.Field);
            // Construir constant correctamente según el operador/tipo
            Expression constant;
            if (baseFilter.Operator == FilterOperator.In)
            {
                var enumerableType = typeof(IEnumerable<>).MakeGenericType(member.Type);
                constant = Expression.Constant(baseFilter.Value, enumerableType);
            }
            else
            {
                var rawValue = baseFilter.Value;
                if (rawValue == null)
                {
                    constant = Expression.Constant(null, member.Type);
                }
                else if (member.Type == typeof(Guid))
                {
                    if (rawValue is Guid g) constant = Expression.Constant(g, typeof(Guid));
                    else constant = Expression.Constant(Guid.Parse(rawValue.ToString()!), typeof(Guid));
                }
                else if (member.Type.IsEnum)
                {
                    var enumVal = Enum.Parse(member.Type, rawValue.ToString()!);
                    constant = Expression.Constant(enumVal, member.Type);
                }
                else
                {
                    var converted = Convert.ChangeType(rawValue, member.Type);
                    constant = Expression.Constant(converted, member.Type);
                }
            }
            
            Expression body = baseFilter.Operator switch
            {
                FilterOperator.Eq => Expression.Equal(member, constant),
                FilterOperator.Neq => Expression.NotEqual(member, constant),
                FilterOperator.Gt => Expression.GreaterThan(member, constant),
                FilterOperator.Gte => Expression.GreaterThanOrEqual(member, constant),
                FilterOperator.Lt => Expression.LessThan(member, constant),
                FilterOperator.Lte => Expression.LessThanOrEqual(member, constant),
                FilterOperator.Contains => Expression.Call(member, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant),
                FilterOperator.In => Expression.Call(
                    typeof(System.Linq.Enumerable),
                    nameof(System.Linq.Enumerable.Contains),
                    new[] { member.Type },
                    Expression.Constant(baseFilter.Value, typeof(IEnumerable<>).MakeGenericType(member.Type)),
                    member),
                _ => throw new NotImplementedException()
            };
    
            var lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return query.Where(lambda);
        }
        else if (filter is AndFilter andFilter)
        {
            foreach (var subFilter in andFilter.Value)
            {
                query = ApplyFilter(query, subFilter);
            }
            return query;
        }
        else if (filter is OrFilter orFilter)
        {
            var param = Expression.Parameter(typeof(T), "x");
            Expression? body = null;
    
            foreach (var subFilter in orFilter.Value)
            {
                var subQuery = ApplyFilter(_dbSet.AsQueryable(), subFilter);
                var subExpr = ((Expression<Func<T, bool>>)subQuery.Expression).Body;
    
                body = body == null ? subExpr : Expression.OrElse(body, subExpr);
            }
    
            if (body == null) return query;
            var lambda = Expression.Lambda<Func<T, bool>>(body, param);
            return query.Where(lambda);
        }
    
        return query;
    }
}