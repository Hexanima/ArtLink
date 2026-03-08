using Application.Mocks.Entities;

namespace Application.Tests;

public class LoginUseCaseTests
{
    [Fact]
    public void DESCRIPTION()
    {
        User user = MockUser.Generate();
        Assert.IsType<string>(user.Email);
    }
}
