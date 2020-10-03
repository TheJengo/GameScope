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
    public class DeleteRatingCommandHandler : IRequestHandler<DeleteRatingCommand, bool>
    {
        private readonly IRatingRepository _ratingRepository;

        public DeleteRatingCommandHandler(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public Task<bool> Handle(DeleteRatingCommand request, CancellationToken cancellationToken)
        {
            var rating = _ratingRepository.GetSingle(x => x.UserId == request.UserId && x.GameId == request.GameId);
            _ratingRepository.Remove(rating);

            return Task.FromResult(_ratingRepository.SaveChanges() > 0);
        }
    }
}
