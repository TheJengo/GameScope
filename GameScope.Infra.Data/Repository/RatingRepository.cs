using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using GameScope.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.Data.Repository
{
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(GameScopeContext context) : base(context)
        {
        }
    }
}
