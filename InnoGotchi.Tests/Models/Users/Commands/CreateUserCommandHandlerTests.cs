using InnoGotchi.Application.Models.Users.Commands.CreateUser;
using InnoGotchi.Application.Interfaces;
using InnoGotchi.Domain;

namespace InnoGotchi.Tests.Models.Users.Commands
{
    public class CreateUserCommandHandlerTests
    {
        Mock<IUsersDbContext> mock = new Mock<IUsersDbContext>();
        Fixture Fixture = new Fixture();
        [Fact]
        public async Task CreateUserCommandHandler_Success()
        {
            IList<User> users = new List<User>() { };
            mock.Setup(x => x.Users).ReturnsDbSet(users);

            CreateUserCommand createUser = new CreateUserCommand
            {
                Email = "testEmail@gmail.com",
                FirstName = Fixture.Create<string>(),
                LastName = Fixture.Create<string>(),
                Password = Fixture.Create<string>(),
                Avatar = Fixture.Create<byte[]>()
            };
            var handler = new CreateUserCommandHandler(mock.Object);

            Guid userId;
            try
            {
                userId = await handler.Handle(createUser, CancellationToken.None);
            }
            catch { userId = Guid.Empty; }



            userId.Should().NotBe(Guid.Empty);
        }
        [Fact]
        public async Task CreateUserCommandHandler_EmailAlreadyExists()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Email = "testEmail@gmail.com",
                FirstName = Fixture.Create<string>(),
                LastName = Fixture.Create<string>(),
                Password = Fixture.Create<string>(),
                Avatar = Fixture.Create<byte[]>(),
                Role = "user"
            };
            IList<User> users = new List<User>() { user };
            mock.Setup(x => x.Users).ReturnsDbSet(users);

            var handler = new CreateUserCommandHandler(mock.Object);
            CreateUserCommand createUser = new CreateUserCommand
            {
                Email = "testEmail@gmail.com",
                FirstName = Fixture.Create<string>(),
                LastName = Fixture.Create<string>(),
                Password = Fixture.Create<string>(),
                Avatar = Fixture.Create<byte[]>()
            };
            
            
            Guid userId;
            try
            {
                userId = await handler.Handle(createUser, CancellationToken.None);
            }
            catch { userId = Guid.Empty; }



            userId.Should().Be(Guid.Empty);
        }
    }
}
