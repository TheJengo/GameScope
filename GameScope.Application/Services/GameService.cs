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
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMediatorHandler bus, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public void Add(GameCreateViewModel gameCreateViewModel)
        {
            var createGameCommand = _mapper.Map<CreateGameCommand>(gameCreateViewModel);

            _bus.SendCommand(createGameCommand);
        }

        public void Update(GameUpdateViewModel gameUpdateViewModel)
        {
            var updateGameCommand = _mapper.Map<UpdateGameCommand>(gameUpdateViewModel);

            _bus.SendCommand(updateGameCommand);
        }

        public void Delete(int id, int userId)
        {
            var updateGameCommand = new DeleteGameCommand(id, userId);

            _bus.SendCommand(updateGameCommand);
        }

        public IList<GameListViewModel> GetAll()
        {
            var games = _gameRepository.GetAll(g => g.User, g => g.Ratings);

            return _mapper.Map<IList<GameListViewModel>>(games);
        }
    }
}
