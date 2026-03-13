using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatUsersUseCaseTest
{
    [Fact]
    public async Task Should_Return_ChatUsers_When_Found()
    {
        ChatUser[] chatUsers =
        {
            new ChatUserMock().Create(),
            new ChatUserMock().Create(),
            new ChatUserMock().Create()
        };

        var mockRepo = new Mock<IRepository<ChatUser>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(chatUsers.ToList());

        var service = new MockService<ChatUser>(mockRepo.Object);
        var useCase = new GetChatUsersUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(chatUsers.Length, result.Value!.Length);
    }
}
