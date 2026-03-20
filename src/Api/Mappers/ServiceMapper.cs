using Application.DTOs;
using Domain.Entities;

namespace Api.Mappers;

public class ServiceMapper : IMapper<ServiceDTO, Service>
{
    public Service Map(ServiceDTO dto)
    {
        return new Service
        {
            Id = Guid.NewGuid(),
            ServiceName = dto.ServiceName,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };
    }
}
