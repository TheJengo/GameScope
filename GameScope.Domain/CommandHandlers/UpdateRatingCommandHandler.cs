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
    public class UpdateRatingCommandHandler : IRequestHandler<UpdateRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;

        public UpdateRatingCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Task<bool> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    var error = request.ValidationResult.Errors[0];

                    throw new GameScopeException(error.ErrorCode, error.ErrorMessage);
                }

                var rating = _ratingRepository.GetSingle(x => x.UserId == request.UserId && x.GameId == request.GameId);

                if (rating == null)
                {
                    throw new GameScopeException("rating_not_found", $"You don't have a related rating to this game");
                }

                rating.Value = request.Value;
                rating.UpdatedDate = request.TimeStamp;

                _ratingRepository.Update(rating);

                return Task.FromResult(_ratingRepository.SaveChanges() > 0);
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
