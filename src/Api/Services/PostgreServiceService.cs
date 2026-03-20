using System.Linq.Expressions;
using Api.ApiEntities;
using Api.Data;
using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PostgreServiceService : IService<Service>
{
    private readonly AppDbContext _ctx;
    private readonly DbSet<ServiceApi> _dbSet;

    public PostgreServiceService(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = ctx.ServiceApi;
    }

    public async Task<OperationResult> Create(Service create)
    {
        try
        {
            await _dbSet.AddAsync(ToApiEntity(create));
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
        try
        {
            ServiceApi? entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                return new OperationResult(new Exception("Not found"));
            }

            _dbSet.Remove(entity);
            await _ctx.SaveChangesAsync();

            return new OperationResult();
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }

    public async Task<OperationResult<Service[]>> GetAll()
    {
        try
        {
            Service[] items = await _dbSet
                .Select(service => ToDomainEntity(service))
                .ToArrayAsync();

            return new OperationResult<Service[]>(items);
        }
        catch (Exception ex)
        {
            return new OperationResult<Service[]>(ex);
        }
    }

    public async Task<OperationResult<Service>> GetById(Guid id)
    {
        Service? item = await _dbSet
            .Where(service => service.Id == id)
            .Select(service => ToDomainEntity(service))
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return new OperationResult<Service>(new Exception("Not found"));
        }

        return new OperationResult<Service>(item);
    }

    public async Task<OperationResult> Update(Service update)
    {
        try
        {
            ServiceApi? existing = await _dbSet.FindAsync(update.Id);
            if (existing == null)
            {
                return new OperationResult(new Exception("Not found"));
            }

            existing.ServiceName = update.ServiceName;
            existing.CreatedAt = update.CreatedAt;
            existing.UpdatedAt = update.UpdatedAt;
            existing.DeletedAt = update.DeletedAt;

            _dbSet.Update(existing);
            await _ctx.SaveChangesAsync();
            return new OperationResult();
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }

    public async Task<OperationResult<Service[]>> GetMany(Query query)
    {
        try
        {
            IQueryable<ServiceApi> services = _dbSet.AsQueryable();

            if (!query.IncludeDeleted)
            {
                services = services.Where(service => service.DeletedAt == null);
            }

            if (query.Filters != null)
            {
                services = ApplyFilter(services, query.Filters);
            }

            if (query.Offset.HasValue)
            {
                services = services.Skip(query.Offset.Value);
            }

            if (query.Limit.HasValue)
            {
                services = services.Take(query.Limit.Value);
            }

            Service[] result = await services
                .Select(service => ToDomainEntity(service))
                .ToArrayAsync();

            return new OperationResult<Service[]>(result);
        }
        catch (Exception ex)
        {
            return new OperationResult<Service[]>(ex);
        }
    }

    public async Task<OperationResult<Service>> GetOne(Query query)
    {
        try
        {
            IQueryable<ServiceApi> services = _dbSet.AsQueryable();

            if (!query.IncludeDeleted)
            {
                services = services.Where(service => service.DeletedAt == null);
            }

            if (query.Filters != null)
            {
                services = ApplyFilter(services, query.Filters);
            }

            Service? item = await services
                .Select(service => ToDomainEntity(service))
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return new OperationResult<Service>(new Exception("Not found"));
            }

            return new OperationResult<Service>(item);
        }
        catch (Exception ex)
        {
            return new OperationResult<Service>(ex);
        }
    }

    private IQueryable<ServiceApi> ApplyFilter(IQueryable<ServiceApi> query, IFilter filter)
    {
        if (filter is BaseFilter baseFilter)
        {
            var param = Expression.Parameter(typeof(ServiceApi), "x");
            var member = Expression.PropertyOrField(param, baseFilter.Field);

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
                    constant = rawValue is Guid guidValue
                        ? Expression.Constant(guidValue, typeof(Guid))
                        : Expression.Constant(Guid.Parse(rawValue.ToString()!), typeof(Guid));
                }
                else if (member.Type.IsEnum)
                {
                    var enumValue = Enum.Parse(member.Type, rawValue.ToString()!);
                    constant = Expression.Constant(enumValue, member.Type);
                }
                else
                {
                    var targetType = Nullable.GetUnderlyingType(member.Type) ?? member.Type;
                    var converted = Convert.ChangeType(rawValue, targetType);
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
                FilterOperator.Contains => Expression.Call(
                    member,
                    typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!,
                    constant),
                FilterOperator.In => Expression.Call(
                    typeof(Enumerable),
                    nameof(Enumerable.Contains),
                    new[] { member.Type },
                    Expression.Constant(baseFilter.Value, typeof(IEnumerable<>).MakeGenericType(member.Type)),
                    member),
                _ => throw new NotImplementedException()
            };

            var lambda = Expression.Lambda<Func<ServiceApi, bool>>(body, param);
            return query.Where(lambda);
        }

        if (filter is AndFilter andFilter)
        {
            foreach (IFilter subFilter in andFilter.Value)
            {
                query = ApplyFilter(query, subFilter);
            }

            return query;
        }

        if (filter is OrFilter orFilter)
        {
            IQueryable<ServiceApi> result = query.Where(_ => false);

            foreach (IFilter subFilter in orFilter.Value)
            {
                IQueryable<ServiceApi> filteredQuery = ApplyFilter(query, subFilter);
                result = result.Union(filteredQuery);
            }

            return result;
        }

        return query;
    }

    private static Service ToDomainEntity(ServiceApi service)
    {
        return new Service
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt,
            DeletedAt = service.DeletedAt
        };
    }

    private static ServiceApi ToApiEntity(Service service)
    {
        return new ServiceApi
        {
            Id = service.Id,
            ServiceName = service.ServiceName,
            CreatedAt = service.CreatedAt,
            UpdatedAt = service.UpdatedAt,
            DeletedAt = service.DeletedAt
        };
    }
}