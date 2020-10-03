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
    public class RatingServiceTests
    {
        private Mock<IRatingRepository> _ratingRepositoryMock;
        private Mock<IMediatorHandler> _mediatorBusMock;
        private Mock<IMapper> _autoMapperMock;
        private RatingService _ratingService;

        public RatingServiceTests()
        {
            _ratingRepositoryMock = new Mock<IRatingRepository>();
            _mediatorBusMock = new Mock<IMediatorHandler>();
            _autoMapperMock = new Mock<IMapper>();
            _ratingService = new RatingService(_ratingRepositoryMock.Object, _mediatorBusMock.Object, _autoMapperMock.Object);
        }

        [Fact]
        public async Task rating_service_add_should_succeed()
        {
            var ratingAddViewModel = new RatingAddViewModel
            {
                UserId = 1,
                GameId = 1,
                Value = 1
            };

            _ratingService.Add(ratingAddViewModel);
            _autoMapperMock.Verify(x => x.Map<CreateRatingCommand>(ratingAddViewModel), Times.Once);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<CreateRatingCommand>()), Times.Once);
        }

        [Fact]
        public async Task rating_service_update_should_succeed()
        {
            var ratingUpdateViewModel = new RatingUpdateViewModel
            {
                UserId = 1,
                GameId = 1,
                Value = 2
            };

            _ratingService.Update(ratingUpdateViewModel);
            _autoMapperMock.Verify(x => x.Map<UpdateRatingCommand>(ratingUpdateViewModel), Times.Once);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<UpdateRatingCommand>()), Times.Once);
        }

        [Fact]
        public async Task rating_service_delete_should_succeed()
        {
            var gameId = 1;
            var userId = 1;
            var requestedUserId = 1;
            _ratingService.Delete(userId, gameId, requestedUserId);
            _mediatorBusMock.Verify(x => x.SendCommand(It.IsAny<DeleteRatingCommand>()), Times.Once);
        }

        [Fact]
        public async Task rating_service_get_all_should_succeed()
        {
            var ratings = new List<Rating>()
            {
                new Rating
                {
                    UserId=1,
                    GameId=1,
                    Value=2,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow
                }
            };

            _ratingRepositoryMock.Setup(x => x.GetAll(It.IsAny<Expression<Func<Rating, object>>>(), It.IsAny<Expression<Func<Rating, object>>>())).Returns(ratings);
            _ratingService.GetAll();
            _ratingRepositoryMock.Verify(x => x.GetAll(It.IsAny<Expression<Func<Rating, object>>>(), It.IsAny<Expression<Func<Rating, object>>>()), Times.Once);
            _autoMapperMock.Verify(x => x.Map<IList<RateListViewModel>>(ratings), Times.Once);
        }
    }
}
