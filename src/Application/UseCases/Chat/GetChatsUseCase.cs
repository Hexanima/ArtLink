using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class GetChatsUseCase
{
    private readonly IService<Chat> _service;

    public GetChatsUseCase(IService<Chat> service)
    {
        _service = service;
    }

    public async Task<OperationResult<Chat[]>> Execute()
    {
        var result = await _service.GetAll();

        if (!result.IsSuccess || result.Value == null)
        {
            return new OperationResult<Chat[]>(new Exception("No chats found"));
        }

        return new OperationResult<Chat[]>(result.Value);
    }
}
