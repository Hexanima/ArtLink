using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class DeleteUserUseCaseTest
{
    [Fact]
    public async Task Should_Set_DeletedAt_When_User_Is_Deleted()
    {
        var userId = Guid.NewGuid();

        User user = new UserMock(
            id: userId
        ).Create();

        var mockRepo = new Mock<IRepository<User>>();
        mockRepo.Setup(r => r.GetById(userId)).ReturnsAsync(user);
        mockRepo.Setup(r => r.Update(It.IsAny<User>(), userId))
                .Callback<User, Guid>((updated, id) => user = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<User>(mockRepo.Object);
        var useCase = new DeleteUserUseCase(service);

        await useCase.Execute(userId);

        Assert.NotNull(user.DeletedAt);
        Assert.True(user.DeletedAt <= DateTime.UtcNow);
    }
}
