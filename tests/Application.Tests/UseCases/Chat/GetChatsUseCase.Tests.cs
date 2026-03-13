using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatsUseCaseTest
{
    [Fact]
    public async Task Should_Return_Chats_When_Found()
    {
        Chat[] chats =
        {
            new ChatMock().Create(),
            new ChatMock().Create(),
            new ChatMock().Create()
        };

        var mockRepo = new Mock<IRepository<Chat>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(chats.ToList());

        var service = new MockService<Chat>(mockRepo.Object);
        var useCase = new GetChatsUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(chats.Length, result.Value!.Length);
    }
}
