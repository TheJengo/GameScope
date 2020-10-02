using GameScope.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Commands
{
    public class CreateUserCommand : Command
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedDate { get; protected set; }

        public CreateUserCommand()
        {
            CreatedAt = TimeStamp;
            UpdatedDate = null;
        }
    }
}
