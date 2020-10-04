using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class UpdateGameCommand : GameCommand
    {
        public UpdateGameCommand(int id, int userId, string name, string description, DateTime? releaseDate)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            ReleaseDate = releaseDate;
            UpdatedAt = TimeStamp;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateGameCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
