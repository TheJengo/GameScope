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
    public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;

        public CreateRatingCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Task<bool> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    var error = request.ValidationResult.Errors[0];

                    throw new GameScopeException(error.ErrorCode, error.ErrorMessage);
                }

                var rating = new Rating
                {
                    UserId = request.UserId,
                    GameId = request.GameId,
                    Value = request.Value,
                    CreatedDate = request.TimeStamp,
                    UpdatedDate = null
                };

                _ratingRepository.Add(rating);

                return Task.FromResult(_ratingRepository.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
