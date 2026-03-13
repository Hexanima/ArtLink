using Application.UseCases;
using Domain.Entities;
using Domain.Services;
using Moq;
using Tests.Mocks.Entities;
using Xunit;

namespace Test.Services;

public class GetChatInvitesUseCaseTest
{
    [Fact]
    public async Task Should_Return_ChatInvites_When_Found()
    {
        ChatInvite[] chatInvites =
        {
            new ChatInviteMock().Create(),
            new ChatInviteMock().Create(),
            new ChatInviteMock().Create()
        };

        var mockRepo = new Mock<IRepository<ChatInvite>>();
        mockRepo.Setup(r => r.GetAll())
                .ReturnsAsync(chatInvites.ToList());

        var service = new MockService<ChatInvite>(mockRepo.Object);
        var useCase = new GetChatInvitesUseCase(service);

        var result = await useCase.Execute();

        Assert.True(result.IsSuccess);
        Assert.Equal(chatInvites.Length, result.Value!.Length);
    }
}
