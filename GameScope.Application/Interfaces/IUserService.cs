using GameScope.Application.ViewModels;
using GameScope.Infra.Common.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.Interfaces
{
    public interface IUserService
    {
        void Register(UserRegisterViewModel userRegisterViewModel);
        JsonWebToken Login(string email, string password);
        UserDetailsViewModel GetById(int userId);
    }
}
