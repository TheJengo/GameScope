using AutoMapper;
using FluentAssertions;
using GameScope.Application.Services;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using GameScope.Infra.Common.Auth;
using GameScope.Infra.Common.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameScope.Application.Tests.Unit.Services
{
    public class UserServiceTests
    {

        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IEncrypter> _encrypterMock;
        private Mock<IJwtHandler> _jwtHandlerMock;
        private Mock<IMediatorHandler> _mediatorBusMock;
        private Mock<IMapper> _autoMapperMock;
        private UserService _userService;

        public UserServiceTests()
        {
            _encrypterMock = new Mock<IEncrypter>();
            _jwtHandlerMock = new Mock<IJwtHandler>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mediatorBusMock = new Mock<IMediatorHandler>();
            _autoMapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _mediatorBusMock.Object, _encrypterMock.Object, _jwtHandlerMock.Object, _autoMapperMock.Object);
        }

        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@gmail.com";
            var password = "test";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            _encrypterMock.Setup(x => x.GetSalt()).Returns(salt);
            _encrypterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            _jwtHandlerMock.Setup(x => x.Create(It.IsAny<int>())).Returns(new JsonWebToken { Token = token });

            var user = new User();
            user.Email = email;
            user.Password = hash;
            user.Salt = salt;
            _userRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Func<User, bool>>())).Returns(user);

            var jwt = _userService.Login(email, password);
            _userRepositoryMock.Verify(x => x.GetSingle(It.IsAny<Func<User, bool>>()), Times.Once);
            _jwtHandlerMock.Verify(x => x.Create(It.IsAny<int>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().Be(token);
        }

        [Fact]
        public async Task user_service_register_should_succeed()
        {
            _userRepositoryMock.Verify(x => x.GetSingle(It.IsAny<Func<User, bool>>()), Times.Once);
            _encrypterMock.Verify(x => x.GetSalt(), Times.Once);
            _encrypterMock.Verify(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<CreateUserCommand>()), Times.Once);
        }
    }
}
