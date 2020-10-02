using GameScope.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class CreateRatingCommand : Command
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }
    }
}
