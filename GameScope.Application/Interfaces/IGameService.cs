using GameScope.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.Interfaces
{
    public interface IGameService
    {
        IList<GameListViewModel> GetAll();
        GameDetailsViewModel GetById(int id);
        void Add(GameCreateViewModel gameCreateViewModel);
        void Update(GameUpdateViewModel gameUpdateViewModel);
        void Delete(int id, int userId);
    }
}
