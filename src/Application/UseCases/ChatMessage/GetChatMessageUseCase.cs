using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatMessageUseCase
{
    private readonly IService<ChatMessage> _service;

    public GetChatMessageUseCase(IService<ChatMessage> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ChatMessage>> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult<ChatMessage>(new ArgumentException("Id cannot be empty"));
        }

        var result = await _service.GetById(id);

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ChatMessage>(new Exception("Chat message not found"));
        }

        return new OperationResult<ChatMessage>(result.Value);
    }
}
