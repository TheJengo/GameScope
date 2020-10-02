using GameScope.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class DeleteGameCommand : Command
    {
        public int Id { get; protected set; }
        public int UserId { get; protected set; }

        public DeleteGameCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
