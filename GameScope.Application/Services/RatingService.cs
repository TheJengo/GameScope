using AutoMapper;
using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IMediatorHandler bus, IMapper mapper)
        {
            _ratingRepository = ratingRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public void Add(RatingAddViewModel ratingAddViewModel)
        {
            var createRatingCommand = _mapper.Map<CreateRatingCommand>(ratingAddViewModel);

            _bus.SendCommand(createRatingCommand);
        }

        public void Update(RatingUpdateViewModel ratingUpdateViewModel, int requestedUserId)
        {
            var updateRatingCommand = _mapper.Map<UpdateRatingCommand>(ratingUpdateViewModel);
            updateRatingCommand.RequestedUserId = requestedUserId;

            _bus.SendCommand(updateRatingCommand);
        }

        public void Delete(int userId, int gameId, int requestedUserId)
        {
            var deleteRatingCommand = new DeleteRatingCommand
            {
                UserId = userId,
                GameId = gameId,
                RequestedUserId = requestedUserId
            };

            _bus.SendCommand(deleteRatingCommand);
        }

        public IList<RateListViewModel> GetAll()
        {
            var ratings = _ratingRepository.GetAll(g => g.User, g => g.Game);

            return _mapper.Map<IList<RateListViewModel>>(ratings);
        }
    }
}
