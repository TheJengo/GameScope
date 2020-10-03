using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.ViewModels
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<UserGameListViewModel> Games{ get; set; }
        public List<UserRatingListView> Ratings{ get; set; }
    }
}
