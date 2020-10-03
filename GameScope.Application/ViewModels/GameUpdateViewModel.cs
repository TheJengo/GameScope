using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class GameUpdateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
