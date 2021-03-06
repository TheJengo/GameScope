﻿using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class UpdateRatingCommandValidation : RatingValidation<UpdateRatingCommand>
    {
        public UpdateRatingCommandValidation()
        {
            ValidateGameId();
            ValidateUserId();
            ValidateValue();
        }
    }
}
