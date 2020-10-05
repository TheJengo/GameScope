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
            CreateMap<GameCreateViewModel, CreateGameCommand>()
                .ConstructUsing(x => new CreateGameCommand(x.UserId, x.Name, x.Description, x.ReleaseDate));
            CreateMap<GameUpdateViewModel, UpdateGameCommand>()
                .ConstructUsing(x => new UpdateGameCommand(x.Id, x.UserId, x.Name, x.Description, x.ReleaseDate));
            CreateMap<RatingAddViewModel, CreateRatingCommand>()
                .ConstructUsing(x => new CreateRatingCommand(x.UserId, x.GameId, x.Value));
            CreateMap<RatingUpdateViewModel, UpdateRatingCommand>()
                .ConstructUsing(x => new UpdateRatingCommand(x.UserId, x.GameId, x.Value));
        }
    }
}
