using Application.DTOs;
using Domain.Entities;
using Domain.Services;
using Domain.Types;

namespace Application.UseCases.Auth;

public class LoginUseCase : IUseCase<LoginDTO, string>
{
    IUserService _userService;
    ICryptoService _cryptoService;

    public LoginUseCase(IUserService userService, ICryptoService cryptoService)
    {
        _userService = userService;
        _cryptoService = cryptoService;
    }

    public async Task<OperationResult<string>> Execute(LoginDTO payload)
    {
        OperationResult<User> userResult = await _userService.GetOne(
            new()
            {
                Filters = new BaseFilter()
                {
                    Operator = FilterOperator.Eq,
                    Field = nameof(User.Email),
                    Value = payload.email
                }
            }
        );

        if (!userResult.IsSuccess || userResult.Value is null)
        {
            return new OperationResult<string>(new Exception());
        }

        OperationResult passwordCheck = await _cryptoService.CompareHash(
            payload.password,
            userResult.Value.HashedPassword
        );

        if (!passwordCheck.IsSuccess)
        {
            return new OperationResult<string>(new Exception());
        }

        return await _cryptoService.GenerateAccessJWT(userResult.Value.ToSecureUser());
    }
}
