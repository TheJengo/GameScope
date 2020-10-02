using GameScope.Domain.Commands;
using GameScope.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameScope.Domain.CommandHandlers
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            //var game = _gameRepository.GetGame(x => x.Id == request.Id);
            // _gameRepository.Remove(game);

            //return Task.FromResult(_gameRepository.SaveChanges() > 0);
            return Task.FromResult(true);
        }
    }
}
