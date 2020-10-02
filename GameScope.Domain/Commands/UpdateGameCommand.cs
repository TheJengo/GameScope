using GameScope.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class UpdateGameCommand : Command
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? UpdatedAt { get; protected set; }

        public UpdateGameCommand()
        {
            UpdatedAt = TimeStamp;
        }
    }
}
