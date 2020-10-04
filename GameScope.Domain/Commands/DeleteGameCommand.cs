using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class DeleteGameCommand : GameCommand
    {
        public DeleteGameCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
        public override bool IsValid()
        {
            ValidationResult = new DeleteGameCommandValidation().Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
