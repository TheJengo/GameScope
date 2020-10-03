using AutoMapper;
using GameScope.Application.ViewModels;
using GameScope.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameScope.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Game, GameListViewModel>()
                .ForMember(dest => dest.RatingAverage, opt => opt.MapFrom(src => src.Ratings.Select(x=>x.Value).Average()))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap()
                .ForPath(s => s.User.Email, opt => opt.Ignore())
                .ForPath(s => s.Ratings, opt => opt.Ignore());
        }
    }
}
