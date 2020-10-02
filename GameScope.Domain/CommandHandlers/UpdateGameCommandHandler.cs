using GameScope.Domain.Commands;
using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameScope.Domain.CommandHandlers
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;

        public UpdateGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<bool> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            //var game = _gameRepository.GetGame(x => x.Id == request.Id);
            //game.Name = request.Name;
            //game.Description = request.Description;
            //game.ReleaseDate = request.ReleaseDate;
            //game.UpdatedDate = request.UpdatedAt;

            // _gameRepository.Update(game);

            //return Task.FromResult(_gameRepository.SaveChanges() > 0);
            return Task.FromResult(true);
        }
    }
}
