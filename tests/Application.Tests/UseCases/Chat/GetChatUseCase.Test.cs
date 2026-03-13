using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatUseCaseTest
{
    [Fact]
    public async Task Should_Return_Chat_When_Found()
    {
        var chatId = Guid.NewGuid();

        Chat chat = new ChatMock(
            id: chatId
        ).Create();

        var mockRepo = new Mock<IRepository<Chat>>();
        mockRepo.Setup(r => r.GetById(chatId))
                .ReturnsAsync(chat);

        var service = new MockService<Chat>(mockRepo.Object);
        var useCase = new GetChatUseCase(service);

        var result = await useCase.Execute(chatId);

        Assert.True(result.IsSuccess);
        Assert.Equal(chatId, result.Value!.Id);
    }
}
