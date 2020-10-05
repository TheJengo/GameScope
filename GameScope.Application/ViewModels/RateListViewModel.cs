using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class RateListViewModel
    {
        public int Value { get; set; }
        public DateTime Date { get; set; }
        public string Owner { get; set; }
        public string Game { get; set; }
    }
}
