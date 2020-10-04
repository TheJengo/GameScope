using FluentValidation;
using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public abstract class GameValidation<T> : AbstractValidator<T> where T : GameCommand
    {
        protected void ValidateId()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0);
        }

        protected void ValidateUserId()
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0);
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(3, 250);
        }

        protected void ValidateDescription()
        {
            RuleFor(x => x.Description)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull()
                .Length(3, 500);
        }
    }
}
