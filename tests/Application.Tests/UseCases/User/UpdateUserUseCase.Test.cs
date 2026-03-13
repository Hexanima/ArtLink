using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class UpdateUserUseCaseTest
{
    [Fact]
    public async Task Should_Set_UpdatedAt_When_User_Is_Updated()
    {
        var userId = Guid.NewGuid();

        User user = new UserMock(
            id: userId,
            createdAt: DateTime.UtcNow.AddMinutes(-1),
            updatedAt: DateTime.UtcNow.AddMinutes(-1),
            fullName: "Original Name"
        ).Create();

        var mockRepo = new Mock<IRepository<User>>();
        mockRepo.Setup(r => r.GetById(userId)).ReturnsAsync(user);
        mockRepo.Setup(r => r.Update(It.IsAny<User>(), userId))
                .Callback<User, Guid>((updated, id) => user = updated)
                .Returns(Task.CompletedTask);

        var service = new MockService<User>(mockRepo.Object);
        var useCase = new UpdateUserUseCase(service);

        user.FullName = "Updated Name";
        await useCase.Execute(user);

        Assert.Equal("Updated Name", user.FullName);
        Assert.True(user.UpdatedAt > user.CreatedAt);
    }
}
