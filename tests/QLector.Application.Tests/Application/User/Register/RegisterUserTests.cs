using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QLector.Application.Core;
using QLector.Application.Users.Register;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QLector.Application.Tests.Application.User.Register
{
    public class RegisterUserTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;
        private readonly ApplicationTestHelper _helper;

        public RegisterUserTests()
        {
            _helper = new ApplicationTestHelper();
            _serviceProvider = _helper.Services;
            _mediator = _serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task User_Register_CanRegister()
        {
            // Arrange
            await _helper.AddDefaultRole();
            var cmd = new RegisterUserCommand
            {
                Email = "User_Register_CanRegister@matty.com",
                Password = "s3cure",
                UserName = "User_Register_CanRegister"
            };
            var request = new Request<RegisterUserCommand, UserCreatedDto>(cmd, null);

            // Act
            var response = await _mediator.Send(request);

            // Assert
            Assert.NotNull(response?.Data);
            Assert.NotNull(response?.Messages);
            Assert.Equal(cmd.UserName, response.Data.UserName);
            Assert.Equal(cmd.Email, response.Data.Email);
            Assert.Contains(response.Messages, x => x.Type == Domain.Core.MessageType.Information);
        }
    }
}
