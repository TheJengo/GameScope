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
            var game = _gameRepository.GetById(request.Id);

            if(game.UserId != request.UserId)
            {
                return Task.FromResult(false);
            }

            _gameRepository.Remove(request.Id);

            return Task.FromResult(_gameRepository.SaveChanges() > 0);
        }
    }
}
