using Domain.Types;

namespace Api.Mappers;

public interface IMapper<TDto, T> where T : IEntity
{
    T Map(TDto dto);
}
