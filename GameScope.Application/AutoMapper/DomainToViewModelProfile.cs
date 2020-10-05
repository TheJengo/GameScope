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
                .ForMember(dest => dest.RatingAverage, opt => opt.MapFrom(src => src.Ratings.ToList().Count > 0 ? src.Ratings.ToList().Select(x => x.Value).Average() : 0))
                .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.Ratings.ToList().Count))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Email))
                .ReverseMap()
                .ForPath(s => s.User.Email, opt => opt.Ignore())
                .ForPath(s => s.Ratings, opt => opt.Ignore());

            CreateMap<Game, UserGameListViewModel>();

            CreateMap<Game, GameDetailsViewModel>()
                .ForMember(dest => dest.RatingAverage, opt => opt.MapFrom(src => src.Ratings.ToList().Count > 0 ? src.Ratings.ToList().Select(x => x.Value).Average() : 0))
                .ForMember(dest => dest.TotalRatings, opt => opt.MapFrom(src => src.Ratings.ToList().Count))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings.ToList()));

            CreateMap<Rating, GameRateListViewModel>()
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? src.UpdatedDate : src.CreatedDate));

            CreateMap<Rating, UserRatingListView>()
                .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? src.UpdatedDate : src.CreatedDate));

            CreateMap<User, UserDetailsViewModel>()
                .ForMember(dest => dest.Games, opt => opt.MapFrom(src => src.Games.ToList()))
                .ForMember(dest => dest.Ratings, opt => opt.MapFrom(src => src.Ratings.ToList()));

            CreateMap<Rating, RateListViewModel>()
                .ForMember(dest => dest.Game, opt => opt.MapFrom(src => src.Game.Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.UpdatedDate.HasValue ? src.UpdatedDate : src.CreatedDate));
        }
    }
}
