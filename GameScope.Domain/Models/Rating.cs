using GameScope.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Models
{
    public class Rating : IEntity
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Relationships
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
