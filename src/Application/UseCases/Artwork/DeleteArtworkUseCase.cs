using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;

public class DeleteArtworkUseCase
{
    private readonly IService<Artwork> _service;

    public DeleteArtworkUseCase(IService<Artwork> service)
    {
        _service = service;
    }

    public async Task Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty");
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            throw new Exception("Artwork not found");
        }

            await _service.Delete(id);

    }
}