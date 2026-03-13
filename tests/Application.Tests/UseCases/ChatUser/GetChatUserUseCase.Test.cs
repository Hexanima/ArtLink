using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatUserUseCaseTest
{
    [Fact]
    public async Task Should_Return_ChatUser_When_Found()
    {
        var chatUserId = Guid.NewGuid();

        ChatUser chatUser = new ChatUserMock(
            id: chatUserId
        ).Create();

        var mockRepo = new Mock<IRepository<ChatUser>>();
        mockRepo.Setup(r => r.GetById(chatUserId))
                .ReturnsAsync(chatUser);

        var service = new MockService<ChatUser>(mockRepo.Object);
        var useCase = new GetChatUserUseCase(service);

        var result = await useCase.Execute(chatUserId);

        Assert.True(result.IsSuccess);
        Assert.Equal(chatUserId, result.Value!.Id);
    }
}
