using GameScope.Domain.Commands;
using GameScope.Domain.Interfaces;
using GameScope.Domain.Models;
using GameScope.Infra.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameScope.Domain.CommandHandlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!request.IsValid())
                {
                    var error = request.ValidationResult.Errors[0];

                    throw new GameScopeException(error.ErrorCode, error.ErrorMessage);
                }

                var user = new User
                {
                    Email = request.Email,
                    Password = request.Password,
                    Salt = request.Salt,
                    CreatedDate = request.TimeStamp,
                    UpdatedDate = null
                };

                _userRepository.Add(user);

                return Task.FromResult(_userRepository.SaveChanges() > 0);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}
