using GameScope.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.Interfaces
{
    public interface IUserService
    {
        void Register(UserRegisterViewModel userRegisterViewModel);
    }
}
