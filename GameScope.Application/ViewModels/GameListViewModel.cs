using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class GameListViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public double RatingAverage { get; set; }
        public int TotalRatings { get; set; }
        public string Owner { get; set; }
    }
}
