using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class CreateRatingCommand : RatingCommand
    {
        public CreateRatingCommand(int userId, int gameId, int value)
        {
            UserId = userId;
            GameId = gameId;
            Value = value;
            CreatedDate = TimeStamp;
            UpdatedDate = null;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateRatingCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
