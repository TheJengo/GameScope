using GameScope.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public abstract class RatingCommand : Command
    {
        public int GameId { get; protected set; }
        public int UserId { get; protected set; }
        public int Value { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime? UpdatedDate { get; protected set; }
    }
}
