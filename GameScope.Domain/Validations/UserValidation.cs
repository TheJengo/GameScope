using FluentValidation;
using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public abstract class UserValidation<T> : AbstractValidator<T> where T : UserCommand
    {
        protected void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .Length(3, 250);
        }

        protected void ValidatePassword()
        {
            RuleFor(x=>x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(3, 250);
        }
    }
}
