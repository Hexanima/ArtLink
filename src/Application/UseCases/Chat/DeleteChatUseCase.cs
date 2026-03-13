using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases;
public class DeleteChatUseCase
{
    private readonly IService<Chat> _service;

    public DeleteChatUseCase(IService<Chat> service)
    {
        _service = service;
    }

    public async Task<OperationResult> Execute(Guid id)
    {
        if (id == Guid.Empty)
        {
            return new OperationResult(new ArgumentException("Id cannot be empty."));
        }

        OperationResult<Chat> existingChatResult = await _service.GetById(id);

        if (!existingChatResult.IsSuccess)
        {
            return new OperationResult(new Exception("Chat not found"));
        }

        return await _service.Delete(id);
    }
}
