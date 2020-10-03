﻿using GameScope.Domain.Commands;
using GameScope.Domain.Interfaces;
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
            //var rating = _ratingRepository.Get(x => x.UserId == request.UserId && x.GameId == request.GameId);
            //rating.Value = request.Value;

            // _ratingRepository.Update(rating);

            //return Task.FromResult(_ratingRepository.SaveChanges() > 0);
            return Task.FromResult(true);
        }
    }
}