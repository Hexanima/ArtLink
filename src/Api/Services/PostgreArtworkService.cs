using System.Linq.Expressions;
using Api.ApiEntities;
using Api.Data;
using Domain.Entities;
using Domain.Services;
using Domain.Types;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class PostgreArtworkService : IService<Artwork>
{
    private readonly AppDbContext _ctx;
    private readonly DbSet<ArtworkApi> _dbSet;

    public PostgreArtworkService(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = ctx.ArtworkApi;
    }

    public async Task<OperationResult> Create(Artwork create)
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
            ArtworkApi? entity = await _dbSet.FindAsync(id);
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

    public async Task<OperationResult<Artwork[]>> GetAll()
    {
        try
        {
            Artwork[] items = await _dbSet
                .Select(artwork => ToDomainEntity(artwork))
                .ToArrayAsync();

            return new OperationResult<Artwork[]>(items);
        }
        catch (Exception ex)
        {
            return new OperationResult<Artwork[]>(ex);
        }
    }

    public async Task<OperationResult<Artwork>> GetById(Guid id)
    {
        Artwork? item = await _dbSet
            .Where(artwork => artwork.Id == id)
            .Select(artwork => ToDomainEntity(artwork))
            .FirstOrDefaultAsync();

        if (item == null)
        {
            return new OperationResult<Artwork>(new Exception("Not found"));
        }

        return new OperationResult<Artwork>(item);
    }

    public async Task<OperationResult> Update(Artwork update)
    {
        try
        {
            ArtworkApi? existing = await _dbSet.FindAsync(update.Id);
            if (existing == null)
            {
                return new OperationResult(new Exception("Not found"));
            }

            existing.OnSale = update.OnSale;
            existing.UserId = update.UserId;
            existing.ServiceId = update.ServiceId;
            existing.ArtworkName = update.ArtworkName;
            existing.Description = update.Description;
            existing.ImageUrl = update.ImageUrl;
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

    public async Task<OperationResult<Artwork[]>> GetMany(Query query)
    {
        try
        {
            IQueryable<ArtworkApi> artworks = _dbSet.AsQueryable();

            if (!query.IncludeDeleted)
            {
                artworks = artworks.Where(artwork => artwork.DeletedAt == null);
            }

            if (query.Filters != null)
            {
                artworks = ApplyFilter(artworks, query.Filters);
            }

            if (query.Offset.HasValue)
            {
                artworks = artworks.Skip(query.Offset.Value);
            }

            if (query.Limit.HasValue)
            {
                artworks = artworks.Take(query.Limit.Value);
            }

            Artwork[] result = await artworks
                .Select(artwork => ToDomainEntity(artwork))
                .ToArrayAsync();

            return new OperationResult<Artwork[]>(result);
        }
        catch (Exception ex)
        {
            return new OperationResult<Artwork[]>(ex);
        }
    }

    public async Task<OperationResult<Artwork>> GetOne(Query query)
    {
        try
        {
            IQueryable<ArtworkApi> artworks = _dbSet.AsQueryable();

            if (!query.IncludeDeleted)
            {
                artworks = artworks.Where(artwork => artwork.DeletedAt == null);
            }

            if (query.Filters != null)
            {
                artworks = ApplyFilter(artworks, query.Filters);
            }

            Artwork? item = await artworks
                .Select(artwork => ToDomainEntity(artwork))
                .FirstOrDefaultAsync();

            if (item == null)
            {
                return new OperationResult<Artwork>(new Exception("Not found"));
            }

            return new OperationResult<Artwork>(item);
        }
        catch (Exception ex)
        {
            return new OperationResult<Artwork>(ex);
        }
    }

    private IQueryable<ArtworkApi> ApplyFilter(IQueryable<ArtworkApi> query, IFilter filter)
    {
        if (filter is BaseFilter baseFilter)
        {
            var param = Expression.Parameter(typeof(ArtworkApi), "x");
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

            var lambda = Expression.Lambda<Func<ArtworkApi, bool>>(body, param);
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
            IQueryable<ArtworkApi> result = query.Where(_ => false);

            foreach (IFilter subFilter in orFilter.Value)
            {
                IQueryable<ArtworkApi> filteredQuery = ApplyFilter(query, subFilter);
                result = result.Union(filteredQuery);
            }

            return result;
        }

        return query;
    }

    private static Artwork ToDomainEntity(ArtworkApi artwork)
    {
    return new Artwork(
        artwork.Id,
        artwork.UserId,
        artwork.ServiceId,
        artwork.ArtworkName,
        artwork.Description,
        artwork.ImageUrl,
        artwork.OnSale,
        artwork.CreatedAt,
        artwork.UpdatedAt,
        artwork.DeletedAt
    );
    }

    private static ArtworkApi ToApiEntity(Artwork artwork)
    {
        return new ArtworkApi
        {
            Id = artwork.Id,
            OnSale = artwork.OnSale,
            UserId = artwork.UserId,
            ServiceId = artwork.ServiceId,
            ArtworkName = artwork.ArtworkName,
            Description = artwork.Description,
            ImageUrl = artwork.ImageUrl,
            CreatedAt = artwork.CreatedAt,
            UpdatedAt = artwork.UpdatedAt,
            DeletedAt = artwork.DeletedAt
        };
    }
}