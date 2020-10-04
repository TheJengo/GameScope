using GameScope.Domain.Commands;
using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using GameScope.Infra.Common.Exceptions;
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
            try
            {
                if (!request.IsValid())
                {
                    var error = request.ValidationResult.Errors[0];

                    throw new GameScopeException(error.ErrorCode, error.ErrorMessage);
                }

                var game = _gameRepository.GetById(request.Id);

                if (game.UserId != request.UserId)
                {
                    throw new GameScopeException("unauthorized_game_update", $"You don't have a permission to edit this game.");
                }

                game.Name = request.Name;
                game.Description = request.Description;
                game.ReleaseDate = request.ReleaseDate;
                game.UpdatedDate = request.UpdatedAt;
                _gameRepository.Update(game);

                return Task.FromResult(_gameRepository.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
