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
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, bool>
    {
        private readonly IGameRepository _gameRepository;

        public CreateGameCommandHandler(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Task<bool> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    var error = request.ValidationResult.Errors[0];

                    throw new GameScopeException(error.ErrorCode, error.ErrorMessage);
                }

                var game = new Game
                {
                    Name = request.Name,
                    UserId = request.UserId,
                    Description = request.Description,
                    ReleaseDate = request.ReleaseDate,
                    CreatedDate = request.CreatedAt,
                    UpdatedDate = request.UpdatedAt
                };

                _gameRepository.Add(game);

                return Task.FromResult(_gameRepository.SaveChanges() > 0);
            }catch(Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
