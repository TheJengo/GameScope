using GameScope.Domain.Core.Bus;
using GameScope.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameScope.Infra.IoC
{
    public static class DependencyResolver
    {
        public static void AddGameScopeIoC(IServiceCollection services)
        {
            // Domain InMemoryBus MediatR Injection
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Domain Handlers Injections
            //services.AddScoped<IRequestHandler<Command, bool>, CommandHandler>();

            // Application Layer Injections

            // Infra Data Layer Injections
        }
    }
}
