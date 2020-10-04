using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class CreateGameCommandValidation : GameValidation<CreateGameCommand>
    {
        public CreateGameCommandValidation()
        {
            ValidateUserId();
            ValidateName();
            ValidateDescription();
        }
    }
}
