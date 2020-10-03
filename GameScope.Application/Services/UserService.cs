using AutoMapper;
using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using GameScope.Infra.Common.Auth;
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
            //var user = await _userRepository.GetAsync(email);

            //if (user != null)
            //{
            //}

            var createUserCommand = new CreateUserCommand
            {
                Email = userRegisterViewModel.Email.ToLowerInvariant()
            };
            createUserCommand.Salt = _encrypter.GetSalt();
            createUserCommand.Password = _encrypter.GetHash(userRegisterViewModel.Password, createUserCommand.Salt);

            _bus.SendCommand(createUserCommand);
        }

        public JsonWebToken Login(string email, string password)
        {
            var user = _userRepository.GetSingle(x => x.Email == email.ToLowerInvariant());

            //if (user == null)
            //{
            //    throw new ActioException("invalid_credentials",
            //        $"Invalid credentials");
            //}

            if (!ValidatePassword(user.Password, user.Salt, password, _encrypter))
            {
                //    throw new ActioException("invalid_credentials",
                //    $"Invalid credentials");
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
