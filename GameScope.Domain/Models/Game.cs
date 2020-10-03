using GameScope.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Models
{
    public class Game : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual User User { get; set; }

        // Relationships
        public virtual ICollection<Rating> Ratings { get; set; }

        public Game()
        {
            Ratings = new HashSet<Rating>();
        }
    }
}
