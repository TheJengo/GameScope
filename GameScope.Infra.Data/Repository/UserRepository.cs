using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using GameScope.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(GameScopeContext context) : base(context)
        {
        }
    }
}
