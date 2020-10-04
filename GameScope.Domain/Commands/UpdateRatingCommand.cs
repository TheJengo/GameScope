using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class UpdateRatingCommand : RatingCommand
    {
        public UpdateRatingCommand(int userId, int gameId, int value, DateTime createdAt)
        {
            UserId = userId;
            GameId = gameId;
            Value = value;
            CreatedDate = createdAt;
            UpdatedDate = TimeStamp;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateRatingCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
