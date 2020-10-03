using GameScope.Domain.CommandHandlers;
using GameScope.Domain.Commands;
using GameScope.Domain.Core.Bus;
using GameScope.Domain.Interfaces;
using GameScope.Infra.Bus;
using GameScope.Infra.Common.Auth;
using GameScope.Infra.Common.Security;
using GameScope.Infra.Data.Context;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.IoC
{
    public static class DependencyResolver
    {
        public static void AddGameScopeIoC(this IServiceCollection services, IConfiguration configuration)
        {
            // Domain InMemoryBus MediatR Injection
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain Handlers Injections
            services.AddScoped<IRequestHandler<CreateGameCommand, bool>, CreateGameCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, CreateUserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateRatingCommand, bool>, CreateRatingCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateGameCommand, bool>, UpdateGameCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateRatingCommand, bool>, UpdateRatingCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteGameCommand, bool>, DeleteGameCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteRatingCommand, bool>, DeleteRatingCommandHandler>();

            // Application Layer Injections

            // Infa Common Layer Injections
            services.AddJwt(configuration);
            services.AddScoped<IEncrypter, Encrypter>();

            // Infra Data Layer Injections
            //services.AddScoped<IUserRepository, UserRepository>();
            //services.AddScoped<IGameRepository, GameRepository>();
            //services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<GameScopeContext>();
        }
    }
}
