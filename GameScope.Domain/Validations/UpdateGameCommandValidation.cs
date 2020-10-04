using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class UpdateGameCommandValidation : GameValidation<UpdateGameCommand>
    {
        public UpdateGameCommandValidation()
        {
            ValidateId();
            ValidateUserId();
            ValidateName();
            ValidateDescription();
        }
    }
}
