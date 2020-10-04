using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class CreateUserCommandValidation : UserValidation<CreateUserCommand>
    {
        public CreateUserCommandValidation()
        {
            ValidateEmail();
            ValidatePassword();
        }
    }
}
