using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetUserUseCaseTest
{
    [Fact]
    public async Task Should_Return_User_When_Found()
    {
        var userId = Guid.NewGuid();

        User user = new UserMock(
            id: userId
        ).Create();

        var mockRepo = new Mock<IRepository<User>>();
        mockRepo.Setup(r => r.GetById(userId))
                .ReturnsAsync(user);

        var service = new MockService<User>(mockRepo.Object);
        var useCase = new GetUserUseCase(service);

        var result = await useCase.Execute(userId);

        Assert.True(result.IsSuccess);
        Assert.Equal(userId, result.Value!.Id);
    }
}
