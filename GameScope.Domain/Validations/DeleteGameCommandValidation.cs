using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public class DeleteGameCommandValidation: GameValidation<DeleteGameCommand>
    {
        public DeleteGameCommandValidation()
        {
            ValidateId();
            ValidateUserId();
        }
    }
}
