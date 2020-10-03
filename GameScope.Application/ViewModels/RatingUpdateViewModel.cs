using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class RatingUpdateViewModel
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int Value { get; set; }
    }
}
