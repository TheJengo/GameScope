using FluentValidation.Results;
using GameScope.Domain.Core.Events;
using System;

namespace GameScope.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        public DateTime TimeStamp { get; protected set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public abstract bool IsValid();
    }
}
