using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class CreateGameCommand : GameCommand
    {
        public CreateGameCommand(int id, int userId, string name, string description, DateTime releaseDate)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Description = description;
            ReleaseDate = releaseDate;
            CreatedAt = TimeStamp;
            UpdatedAt = null;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateGameCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
