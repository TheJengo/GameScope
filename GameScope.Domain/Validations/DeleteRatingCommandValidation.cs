using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class DeleteRatingCommandValidation : RatingValidation<DeleteRatingCommand>
    {
        public DeleteRatingCommandValidation()
        {
            ValidateUserId();
            ValidateGameId();
        }
    }
}
