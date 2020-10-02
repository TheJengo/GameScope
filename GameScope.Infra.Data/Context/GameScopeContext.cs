using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Data.Context
{
    public class GameScopeContext : DbContext
    {
        public GameScopeContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet Collections
    }
}
