using GameScope.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Domain.Models
{
    public class User : IEntity
    {
        public int Id { get; protected set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<Game> Games;
        public ICollection<Rating> Ratings;

        public User()
        {
            Games = new HashSet<Game>();
            Ratings = new HashSet<Rating>();
        }
    }
}
