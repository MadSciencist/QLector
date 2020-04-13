using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QLector.Application.Core;
using QLector.Application.Users.Login;
using System.Threading.Tasks;
using Xunit;

namespace QLector.Application.Tests.Application.User.Login
{
    public class LoginUserTests
    {
        private readonly IMediator _mediator;
        private readonly ApplicationTestHelper _helper;

        public LoginUserTests()
        {
            _helper = new ApplicationTestHelper();
            _mediator = _helper.Services.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task User_Login_ReceivesToken()
        {
            // Arrange
            const string username = nameof(User_Login_ReceivesToken);
            const string password = "s3cure";

            await _helper.AddUser(username, password);
            var cmd = new LoginCommand
            {
                Login = username,
                Password = password
            };
            var request = new CommandRequest<LoginCommand, UserLoggedDto>(cmd, null);

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.NotNull(response?.Data?.Token);
            Assert.StartsWith("ey", response.Data.Token);
        }

        [Fact]
        public async Task User_Login_FailsWhenPasswordInvalid()
        {
            // Arrange
            const string username = nameof(User_Login_FailsWhenPasswordInvalid);
            const string password = "s3cure";

            await _helper.AddUser(username, password);
            var cmd = new LoginCommand
            {
                Login = username,
                Password = "invalid"
            };
            var request = new CommandRequest<LoginCommand, UserLoggedDto>(cmd, null);

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.Null(response?.Data?.Token);
            Assert.Equal(401, response?.GetResponseStatusCodeOverride());
            Assert.Contains(response.Messages, x => x.Type == Domain.Core.MessageType.Error);
        }

        [Fact]
        public async Task User_Login_FailsWhenUserNameInvalid()
        {
            // Arrange
            const string username = nameof(User_Login_FailsWhenUserNameInvalid);
            const string password = "s3cure";

            await _helper.AddUser(username, password);
            var cmd = new LoginCommand
            {
                Login = "invalid",
                Password = password
            };
            var request = new CommandRequest<LoginCommand, UserLoggedDto>(cmd, null);

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.Null(response?.Data?.Token);
            Assert.Equal(401, response?.GetResponseStatusCodeOverride());
            Assert.Contains(response.Messages, x => x.Type == Domain.Core.MessageType.Error);
        }
    }
}
