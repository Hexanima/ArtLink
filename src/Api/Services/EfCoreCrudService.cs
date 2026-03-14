using Api.Data;
using Domain.Types;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using DataAppContext = Api.Data.AppContext;

namespace Api.Services;

public abstract class EfCoreCrudService<TDomain, TApi> : IService<TDomain>
    where TDomain : class, IEntity
    where TApi : class
{
    protected readonly DataAppContext Context;
    protected readonly DbSet<TApi> Set;

    protected EfCoreCrudService(DataAppContext context)
    {
        Context = context;
        Set = context.Set<TApi>();
    }

    protected abstract Guid GetApiId(TApi apiEntity);
    protected abstract void SetApiId(TApi apiEntity, Guid id);
    protected abstract TDomain ToDomain(TApi apiEntity);
    protected abstract TApi ToApi(TDomain domainEntity);
    protected abstract void MapForUpdate(TApi target, TDomain source);

    public async Task<OperationResult> Create(TDomain create)
    {
        try
        {
            var apiEntity = ToApi(create);

            if (GetApiId(apiEntity) == Guid.Empty)
            {
                SetApiId(apiEntity, Guid.NewGuid());
            }

            await Set.AddAsync(apiEntity);
            await Context.SaveChangesAsync();
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
            var entity = await Set.FindAsync(id);
            if (entity is null)
            {
                return new OperationResult(new Exception("Not found"));
            }

            Set.Remove(entity);
            await Context.SaveChangesAsync();
            return new OperationResult();
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }

    public async Task<OperationResult<TDomain[]>> GetAll()
    {
        try
        {
            var query = Set.AsQueryable();
            query = ApplySoftDelete(query, includeDeleted: false);
            var items = await query.ToArrayAsync();
            return new OperationResult<TDomain[]>(items.Select(ToDomain).ToArray());
        }
        catch (Exception ex)
        {
            return new OperationResult<TDomain[]>(ex);
        }
    }

    public async Task<OperationResult<TDomain>> GetById(Guid id)
    {
        try
        {
            var entity = await Set.FindAsync(id);
            if (entity is null)
            {
                return new OperationResult<TDomain>(new Exception("Not found"));
            }

            return new OperationResult<TDomain>(ToDomain(entity));
        }
        catch (Exception ex)
        {
            return new OperationResult<TDomain>(ex);
        }
    }

    public async Task<OperationResult> Update(TDomain update)
    {
        try
        {
            var entity = await Set.FindAsync(update.Id);
            if (entity is null)
            {
                return new OperationResult(new Exception("Not found"));
            }

            MapForUpdate(entity, update);
            Set.Update(entity);
            await Context.SaveChangesAsync();
            return new OperationResult();
        }
        catch (Exception ex)
        {
            return new OperationResult(ex);
        }
    }

    public async Task<OperationResult<TDomain[]>> GetMany(Query query)
    {
        try
        {
            IQueryable<TApi> entities = Set.AsQueryable();
            entities = ApplySoftDelete(entities, query.IncludeDeleted);

            if (query.Filters is not null)
            {
                entities = ApplyFilter(entities, query.Filters);
            }

            if (query.Offset.HasValue)
            {
                entities = entities.Skip(query.Offset.Value);
            }

            if (query.Limit.HasValue)
            {
                entities = entities.Take(query.Limit.Value);
            }

            var result = await entities.ToArrayAsync();
            return new OperationResult<TDomain[]>(result.Select(ToDomain).ToArray());
        }
        catch (Exception ex)
        {
            return new OperationResult<TDomain[]>(ex);
        }
    }

    public async Task<OperationResult<TDomain>> GetOne(Query query)
    {
        try
        {
            IQueryable<TApi> entities = Set.AsQueryable();
            entities = ApplySoftDelete(entities, query.IncludeDeleted);

            if (query.Filters is not null)
            {
                entities = ApplyFilter(entities, query.Filters);
            }

            var entity = await entities.FirstOrDefaultAsync();
            if (entity is null)
            {
                return new OperationResult<TDomain>(new Exception("Not found"));
            }

            return new OperationResult<TDomain>(ToDomain(entity));
        }
        catch (Exception ex)
        {
            return new OperationResult<TDomain>(ex);
        }
    }

    private static IQueryable<TApi> ApplySoftDelete(IQueryable<TApi> query, bool includeDeleted)
    {
        if (includeDeleted)
        {
            return query;
        }

        var deletedAtProp = typeof(TApi).GetProperty(nameof(ISoftDeletedEntity.DeletedAt));
        if (deletedAtProp is null)
        {
            return query;
        }

        var parameter = Expression.Parameter(typeof(TApi), "x");
        var member = Expression.Property(parameter, deletedAtProp);
        var nullConstant = Expression.Constant(null, deletedAtProp.PropertyType);
        var body = Expression.Equal(member, nullConstant);
        var lambda = Expression.Lambda<Func<TApi, bool>>(body, parameter);
        return query.Where(lambda);
    }

    private static IQueryable<TApi> ApplyFilter(IQueryable<TApi> query, IFilter filter)
    {
        var parameter = Expression.Parameter(typeof(TApi), "x");
        var body = BuildFilterExpression(filter, parameter);

        if (body is null)
        {
            return query;
        }

        var lambda = Expression.Lambda<Func<TApi, bool>>(body, parameter);
        return query.Where(lambda);
    }

    private static Expression? BuildFilterExpression(IFilter filter, ParameterExpression parameter)
    {
        return filter switch
        {
            BaseFilter baseFilter => BuildBaseFilterExpression(baseFilter, parameter),
            AndFilter andFilter => BuildAndExpression(andFilter, parameter),
            OrFilter orFilter => BuildOrExpression(orFilter, parameter),
            _ => null
        };
    }

    private static Expression? BuildAndExpression(AndFilter andFilter, ParameterExpression parameter)
    {
        Expression? combined = null;
        foreach (var child in andFilter.Value)
        {
            var childExpr = BuildFilterExpression(child, parameter);
            if (childExpr is null)
            {
                continue;
            }

            combined = combined is null ? childExpr : Expression.AndAlso(combined, childExpr);
        }

        return combined;
    }

    private static Expression? BuildOrExpression(OrFilter orFilter, ParameterExpression parameter)
    {
        Expression? combined = null;
        foreach (var child in orFilter.Value)
        {
            var childExpr = BuildFilterExpression(child, parameter);
            if (childExpr is null)
            {
                continue;
            }

            combined = combined is null ? childExpr : Expression.OrElse(combined, childExpr);
        }

        return combined;
    }

    private static Expression BuildBaseFilterExpression(BaseFilter filter, ParameterExpression parameter)
    {
        var property = typeof(TApi).GetProperty(filter.Field, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
            ?? throw new InvalidOperationException($"Field '{filter.Field}' does not exist on {typeof(TApi).Name}");

        var member = Expression.Property(parameter, property);
        var targetType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

        if (filter.Operator == FilterOperator.In)
        {
            if (filter.Value is not System.Collections.IEnumerable enumerable)
            {
                throw new InvalidOperationException("IN filter requires an enumerable value");
            }

            var convertedValues = new List<object?>();
            foreach (var item in enumerable)
            {
                convertedValues.Add(ConvertValue(item, targetType));
            }

            var typedList = Array.CreateInstance(targetType, convertedValues.Count);
            for (int i = 0; i < convertedValues.Count; i++)
            {
                typedList.SetValue(convertedValues[i], i);
            }

            return Expression.Call(
                typeof(Enumerable),
                nameof(Enumerable.Contains),
                new[] { targetType },
                Expression.Constant(typedList),
                EnsureType(member, targetType));
        }

        var converted = ConvertValue(filter.Value, targetType);
        var constant = Expression.Constant(converted, targetType);
        var comparableMember = EnsureType(member, targetType);

        return filter.Operator switch
        {
            FilterOperator.Eq => Expression.Equal(comparableMember, constant),
            FilterOperator.Neq => Expression.NotEqual(comparableMember, constant),
            FilterOperator.Gt => Expression.GreaterThan(comparableMember, constant),
            FilterOperator.Gte => Expression.GreaterThanOrEqual(comparableMember, constant),
            FilterOperator.Lt => Expression.LessThan(comparableMember, constant),
            FilterOperator.Lte => Expression.LessThanOrEqual(comparableMember, constant),
            FilterOperator.Contains => BuildContainsExpression(comparableMember, constant),
            _ => throw new NotImplementedException($"Unsupported operator '{filter.Operator}'")
        };
    }

    private static Expression BuildContainsExpression(Expression member, ConstantExpression constant)
    {
        if (member.Type != typeof(string))
        {
            throw new InvalidOperationException("Contains operator can only be used with string fields");
        }

        var containsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) })!;
        return Expression.Call(member, containsMethod, constant);
    }

    private static Expression EnsureType(Expression expression, Type type)
    {
        return expression.Type == type ? expression : Expression.Convert(expression, type);
    }

    private static object? ConvertValue(object? raw, Type targetType)
    {
        if (raw is null)
        {
            return null;
        }

        if (targetType == typeof(Guid))
        {
            return raw is Guid guid ? guid : Guid.Parse(raw.ToString()!);
        }

        if (targetType.IsEnum)
        {
            return Enum.Parse(targetType, raw.ToString()!, ignoreCase: true);
        }

        if (targetType == typeof(DateTime))
        {
            return raw is DateTime dt ? dt : DateTime.Parse(raw.ToString()!);
        }

        return Convert.ChangeType(raw, targetType);
    }
}