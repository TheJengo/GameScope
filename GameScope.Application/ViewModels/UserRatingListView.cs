using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class UserRatingListView
    {
        public int GameId { get; set; }
        public int Value { get; set; }
        public string Game { get; set; }
        public DateTime Date { get; set; }
    }
}
