using GameScope.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.Interfaces
{
    public interface IRatingService
    {
        IList<RateListViewModel> GetAll();
        void Add(RatingAddViewModel ratingAddViewModel);
        void Update(RatingUpdateViewModel ratingUpdateViewModel, int requestUserId);
        void Delete(int userId, int gameId, int requestedUserId);
    }
}
