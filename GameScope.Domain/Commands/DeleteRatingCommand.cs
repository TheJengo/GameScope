using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class DeleteRatingCommand : RatingCommand
    {
        public DeleteRatingCommand(int userId, int gameId)
        {
            UserId = userId;
            GameId = gameId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DeleteRatingCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
