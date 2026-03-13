using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatUseCase
{
    private readonly IService<Chat> _service;

    public GetChatUseCase(IService<Chat> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Chat>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<Chat>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Chat>(new Exception("Chat not found"));
        }

        return new OperationResult<Chat>(result.Value);
    }
}
