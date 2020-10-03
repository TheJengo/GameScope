using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _bus;

        public UserService(IUserRepository userRepository, IMediatorHandler bus)
        {
            _userRepository = userRepository;
            _bus = bus;
        }

        public void Create(UserRegisterViewModel userRegisterViewModel)
        {
            var createUserCommand = new CreateUserCommand();

            _bus.SendCommand(createUserCommand);
        }
    }
}
