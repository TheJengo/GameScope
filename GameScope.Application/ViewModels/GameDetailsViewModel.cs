using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class GameDetailsViewModel : GameListViewModel
    {
        public IList<GameRateListViewModel> Ratings { get; set; }
    }
}
