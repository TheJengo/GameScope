using AutoMapper;
using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using GameScope.Infra.Common.Auth;
using GameScope.Infra.Common.Exceptions;
using GameScope.Infra.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameScope.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediatorHandler _bus;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMediatorHandler bus, IEncrypter encrypter, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userRepository = userRepository;
            _bus = bus;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
        }

        public void Register(UserRegisterViewModel userRegisterViewModel)
        {
            var user = _userRepository.GetSingle(x=>x.Email.Equals(userRegisterViewModel.Email.ToLowerInvariant()));

            if (user != null)
            {
                throw new GameScopeException("user_already_exists", "This email already registered");
            }

            var salt = _encrypter.GetSalt();
            var password = _encrypter.GetHash(userRegisterViewModel.Password, salt);

            var createUserCommand = new CreateUserCommand(
             userRegisterViewModel.Email.ToLowerInvariant(),
             password,
             salt
            );

            _bus.SendCommand(createUserCommand);
        }

        public JsonWebToken Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new GameScopeException("invalid_credentials",
                    $"Invalid credentials");
            }

            var user = _userRepository.GetSingle(x => x.Email == email.ToLowerInvariant());

            if (user == null)
            {
                throw new GameScopeException("invalid_credentials",
                    $"Invalid credentials");
            }

            if (!ValidatePassword(user.Password, user.Salt, password, _encrypter))
            {
                throw new GameScopeException("invalid_credentials",
                $"Invalid credentials");
            }

            return _jwtHandler.Create(user.Id);
        }

        public UserDetailsViewModel GetById(int userId)
        {
            var user = _userRepository.GetSingle(x => x.Id == userId, u => u.Games, u => u.Ratings);

            foreach (var rating in user.Ratings)
            {
                rating.Game = user.Games.FirstOrDefault(x => x.Id == rating.GameId);
            }

            return _mapper.Map<UserDetailsViewModel>(user);
        }

        private bool ValidatePassword(string userPassword, string salt, string insertedPassword, IEncrypter encrypter)
            => userPassword.Equals(encrypter.GetHash(insertedPassword, salt));
    }
}
