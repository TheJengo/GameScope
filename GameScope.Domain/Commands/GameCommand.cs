using GameScope.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public abstract class GameCommand : Command
    {
        public int Id { get; protected set; }
        public int UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime? ReleaseDate { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
    }
}
