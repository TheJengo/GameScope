using FluentValidation;
using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Validations
{
    public abstract class RatingValidation<T> : AbstractValidator<T> where T : RatingCommand
    {
        protected void ValidateGameId()
        {
            RuleFor(x => x.GameId)
                .GreaterThan(0);
        }

        protected void ValidateUserId()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0);
        }
        protected void ValidateValue()
        {
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(10);
        }
    }
}
