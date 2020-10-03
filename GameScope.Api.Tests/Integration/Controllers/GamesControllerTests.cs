using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace GameScope.Api.Tests.Integration.Controllers
{
    public class GamesControllerTests
    {
        private readonly TestServer _apiServer;
        private readonly HttpClient _apiClient;

        public GamesControllerTests()
        {
            _apiServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<GameScope.Api.Startup>());
            _apiClient = _apiServer.CreateClient();
        }

        //[Fact]
    }
}
