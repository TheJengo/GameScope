using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class CreateRatingCommandValidation : RatingValidation<CreateRatingCommand>
    {
        public CreateRatingCommandValidation()
        {
            ValidateUserId();
            ValidateGameId();
            ValidateValue();
        }
    }
}
