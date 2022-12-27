namespace Gatherly.Application.UnitTests.Members.Commands;
using Gatherly.Application.Members.Commands;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using Gatherly.Domain.Shared;
using Moq;

public class CreateMemberCommandHandlerTest
{
    private readonly Mock<IMemberRepository> memberRepositoryMock;

    private readonly Mock<IUnitOfWork> unitOfWorkMock;

    public CreateMemberCommandHandlerTest()
    {
        memberRepositoryMock = new();
        unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_ReturnsFailure_WhenEmailIsNotUnique()
    {
        var command = new CreateMemberCommand("email.example.com", "first", "last");

        var handler = new CreateMemberCommandHandler(memberRepositoryMock.Object, unitOfWorkMock.Object);        
        
        memberRepositoryMock.Setup(
            x => x.GetByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(Member.Create(command.FirstName, command.LastName, command.Email));

        var result = await handler.Handle(command, default);

        Assert.IsType<Result<CreateMemberResponse>>(result);
        Assert.True(result.IsFailure);       
        Assert.Equivalent(Domain.Errors.Member.DuplicateEmail, result.FirstError);
    }

    [Fact]
    public async Task Handle_ReturnsSuccessResult_WhenEmailIsUnique()
    {
        var command = new CreateMemberCommand("email.example.com", "first", "last");

        var handler = new CreateMemberCommandHandler(memberRepositoryMock.Object, unitOfWorkMock.Object);        
        
        memberRepositoryMock.Setup(
            x => x.GetByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(default(Member));

        var result = await handler.Handle(command, default);
        var member = result.Value;

        Assert.IsType<Result<CreateMemberResponse>>(result);
        Assert.True(result.IsSuccess);
        Assert.NotNull(member);
        Assert.Equal(command.FirstName, member.FirstName);
        Assert.Equal(command.LastName, member.LastName);
        Assert.Equal(command.Email, member.Email);
    }

    [Fact]
    public async Task Handle_CallsRepositoryAdd_WhenEmailIsUnique()
    {
        var command = new CreateMemberCommand("email.example.com", "first", "last");

        var handler = new CreateMemberCommandHandler(memberRepositoryMock.Object, unitOfWorkMock.Object);

        memberRepositoryMock.Setup(
            x => x.GetByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(default(Member));

        var result = await handler.Handle(command, default);

        memberRepositoryMock.Verify(
            x => x.AddAsync(
                It.Is<Member>(m => m.Email == command.Email && m.FirstName == command.FirstName && m.LastName == command.LastName && m.Id == result.Value.Id),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_DoesNotCallSave_WhenEmailIsNotUnique()
    {
        var command = new CreateMemberCommand("email.example.com", "first", "last");

        var handler = new CreateMemberCommandHandler(memberRepositoryMock.Object, unitOfWorkMock.Object);

        memberRepositoryMock.Setup(
            x => x.GetByEmailAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync(Member.Create(command.FirstName, command.LastName, command.Email));

        var result = await handler.Handle(command, default);

        unitOfWorkMock.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
