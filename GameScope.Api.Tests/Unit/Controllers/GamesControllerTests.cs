using FluentAssertions;
using GameScope.Api.Controllers;
using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace GameScope.Api.Tests.Unit.Controllers
{
    public class GamesControllerTests
    {
        [Fact]
        public async void games_controller_post_should_return_accepted()
        {
            var gameServiceMock = new Mock<IGameService>();
            var controller = new GamesController(gameServiceMock.Object);
            var userId = 1;
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) }
                    , "test"))
                }
            };

            var gameCreateViewModel = new GameCreateViewModel()
            {
                Name="test",
                Description = "test",
                ReleaseDate = DateTime.UtcNow,
                UserId = userId
            };

            var result = controller.Post(gameCreateViewModel);

            var contentResult = result as StatusCodeResult;
            contentResult.Should().NotBeNull();
            contentResult.StatusCode.Should().Be(200);
        }
    }
}
