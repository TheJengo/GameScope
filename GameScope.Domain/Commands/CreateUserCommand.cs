using GameScope.Domain.Core.Commands;
using GameScope.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class CreateUserCommand : UserCommand
    {
        public CreateUserCommand(string email, string password, string salt)
        {
            Email = email;
            Salt = salt;
            Password = password;
            CreatedDate = TimeStamp;
            UpdatedDate = null;
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateUserCommandValidation().Validate(this);
            
            return ValidationResult.IsValid;
        }
    }
}
