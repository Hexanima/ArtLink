using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatMessagesUseCase
{
    private readonly IService<ChatMessage> _service;

    public GetChatMessagesUseCase(IService<ChatMessage> service)
    {
        _service = service;
    }

    public async Task<OperationResult<ChatMessage[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<ChatMessage[]>(new Exception("No chat messages found"));
        }

        return new OperationResult<ChatMessage[]>(result.Value);
    }
}
