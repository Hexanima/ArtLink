using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteChatUserUseCase
{
    private readonly IService<ChatUser> _service;

    public DeleteChatUserUseCase(IService<ChatUser> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<ChatUser> existingChatUserResult = await _service.GetById(id);

        if (!existingChatUserResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat user not found"));
        }

        return await _service.Delete(id);
    }
}
