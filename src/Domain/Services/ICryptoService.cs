using Domain.Types;

namespace Domain.Services;

public interface ICryptoService
{
    public Task<OperationResult<string>> Hash(string text);
    public Task<OperationResult> CompareHash(string text, string hashedText);

    public Task<OperationResult<string>> GenerateAccessJWT(IEntity payload);
    public Task<OperationResult<IEntity>> ParseAccessJWT(string payload);
}
