using AutoMapper;
using FluentAssertions;
using GameScope.Application.Services;
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

        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var autoMapperMock = new Mock<IMapper>();
            var mediatorBusMock = new Mock<IMediatorHandler>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrpyterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            encrpyterMock.Setup(x => x.GetSalt()).Returns(salt);
            encrpyterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<int>())).Returns(new JsonWebToken { Token = token });

            var user = new User();
            user.Email = email;
            user.Password = encrpyterMock.Object.GetHash(password, salt);
            userRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Func<User,bool>>())).Returns(user);

            var userService = new UserService(userRepositoryMock.Object, mediatorBusMock.Object, encrpyterMock.Object, jwtHandlerMock.Object, autoMapperMock.Object);

            var jwt = userService.Login(email, password);
            userRepositoryMock.Verify(x => x.GetSingle(It.IsAny<Func<User, bool>>()), Times.Once);
            jwtHandlerMock.Verify(x => x.Create(It.IsAny<int>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.Should().Be(token);
        }

        //[Fact]
        //public async Task user_service_register_should_succeed()
        //{

        //}
    }
}
