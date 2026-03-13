using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetUsersUseCaseTest
{
    [Fact]
    public async Task Should_Return_Users_When_Found()
    {
        User[] users =
        {
            new UserMock().Create(),
            new UserMock().Create(),
            new UserMock().Create()
        };

        var mockRepo = new Mock<IRepository<User>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(users.ToList());

        var service = new MockService<User>(mockRepo.Object);
        var useCase = new GetUsersUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(users.Length, result.Value!.Length);
    }
}
