using AutoMapper;
using GameScope.Application.Services;
using GameScope.Application.ViewModels;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameScope.Application.Tests.Unit.Services
{
    public class GameServiceTests
    {
        private Mock<IGameRepository> _gameRepositoryMock;
        private Mock<IRatingRepository> _ratingRepositoryMock;
        private Mock<IMediatorHandler> _mediatorBusMock;
        private Mock<IMapper> _autoMapperMock;
        private GameService _gameService;

        public GameServiceTests()
        {
            _gameRepositoryMock = new Mock<IGameRepository>();
            _ratingRepositoryMock = new Mock<IRatingRepository>();
            _mediatorBusMock = new Mock<IMediatorHandler>();
            _autoMapperMock = new Mock<IMapper>();
            _gameService = new GameService(_gameRepositoryMock.Object, _ratingRepositoryMock.Object, _mediatorBusMock.Object, _autoMapperMock.Object);

        }

        [Fact]
        public async Task game_service_add_should_succeed()
        {
            var createGameViewModel = new GameCreateViewModel
            {
                UserId = 1,
                ReleaseDate = DateTime.UtcNow,
                Name = "test",
                Description = "test",
            };

            _gameService.Add(createGameViewModel);
            _autoMapperMock.Verify(x => x.Map<CreateGameCommand>(createGameViewModel), Times.Once);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<CreateGameCommand>()), Times.Once);
        }

        [Fact]
        public async Task game_service_update_should_succeed()
        {
            var gameUpdateViewModel = new GameUpdateViewModel
            {
                Id = 1,
                UserId = 1,
                ReleaseDate = DateTime.UtcNow,
                Name = "test1",
                Description = "test",
            };

            _gameService.Update(gameUpdateViewModel);
            _autoMapperMock.Verify(x => x.Map<UpdateGameCommand>(gameUpdateViewModel), Times.Once);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<UpdateGameCommand>()), Times.Once);
        }

        [Fact]
        public async Task game_service_delete_should_succeed()
        {
            var id = 1;
            var userId = 1;
            _gameService.Delete(id, userId);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<DeleteGameCommand>()), Times.Once);
        }

        [Fact]
        public async Task game_service_get_all_should_succeed()
        {
            var games = new List<Game>()
            {
                new Game
                {
                    Id=1,
                    Name="test",
                    Description = "test",
                    ReleaseDate = DateTime.UtcNow,
                    CreatedDate = DateTime.UtcNow,
                }
            };

            _gameRepositoryMock.Setup(x => x.GetAll(g => g.User, g => g.Ratings)).Returns(games);
            _gameService.GetAll();
            _gameRepositoryMock.Verify(x => x.GetAll(g => g.User, g => g.Ratings), Times.Once);
            _autoMapperMock.Verify(x => x.Map<List<GameListViewModel>>(games), Times.Once);
        }

        [Fact]
        public async Task game_service_get_by_id_should_succeed()
        {
            var ratings = new List<Rating>
            {
                new Rating
                {
                    GameId = 1,
                    UserId = 1,
                    Value = 5,
                    CreatedDate = DateTime.UtcNow
                }
            };

            var game = new Game
            {
                Id = 1,
                Name = "test",
                Description = "test",
                ReleaseDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
            };

            _gameRepositoryMock.Setup(x => x.GetSingle(It.IsAny<Func<Game, bool>>(), It.IsAny<Expression<Func<Game, object>>>())).Returns(game);
            _ratingRepositoryMock.Setup(x => x.GetList(It.IsAny<Func<Rating, bool>>(), It.IsAny<Expression<Func<Rating, object>>>())).Returns(ratings);
            game.Ratings = ratings;

            _gameService.GetById(game.Id);
            _gameRepositoryMock.Verify(x => x.GetSingle(It.IsAny<Func<Game, bool>>(), It.IsAny<Expression<Func<Game, object>>>()), Times.Once);
            _ratingRepositoryMock.Verify(x => x.GetList(It.IsAny<Func<Rating, bool>>(), It.IsAny<Expression<Func<Rating, object>>>()), Times.Once);
            _autoMapperMock.Verify(x => x.Map<GameDetailsViewModel>(game), Times.Once);
        }
    }
}
