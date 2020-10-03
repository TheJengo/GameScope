using AutoMapper;
using GameScope.Application.ViewModels;
using GameScope.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Application.AutoMapper
{
    public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<GameCreateViewModel, CreateGameCommand>();
            CreateMap<GameUpdateViewModel, UpdateGameCommand>();
            CreateMap<UserRegisterViewModel, CreateUserCommand>();
        }
    }
}
