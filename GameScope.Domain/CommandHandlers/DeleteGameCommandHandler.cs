using GameScope.Domain.Commands;
using GameScope.Domain.Interfaces;
using GameScope.Infra.Common.Exceptions;
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
        private readonly IRatingRepository _ratingRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository, IRatingRepository ratingRepository)
        {
            _gameRepository = gameRepository;
            _ratingRepository = ratingRepository;
        }

        public Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    var error = request.ValidationResult.Errors[0];

                    throw new GameScopeException(error.ErrorCode, error.ErrorMessage);
                }

                var game = _gameRepository.GetById(request.Id);

                if (game == null)
                {
                    throw new GameScopeException("game_not_found", $"You don't have a game with given information.");
                }

                if (game.UserId != request.UserId)
                {
                    throw new GameScopeException("unauthorized_game_delete", $"You don't have a permission to edit this game.");
                }

                var ratings = _ratingRepository.GetList(x => x.GameId == request.Id);

                foreach (var rating in ratings)
                {
                    _ratingRepository.Remove(rating);
                }

                _ratingRepository.SaveChanges();
                _gameRepository.Remove(request.Id);

                return Task.FromResult(_gameRepository.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
