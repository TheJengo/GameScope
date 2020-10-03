using FluentAssertions;
using GameScope.Application.ViewModels;
using GameScope.Infra.Common.Auth;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameScope.Api.Tests.Integration.Controllers
{
    public class UsersControllerTests
    {
        private readonly TestServer _apiServer;
        private readonly HttpClient _apiClient;

        public UsersControllerTests()
        {
            _apiServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<GameScope.Api.Startup>());
            _apiClient = _apiServer.CreateClient();
        }

        [Fact]
        public async Task users_controller_register_should_return_accepted()
        {
            var payload = GetPayload(new { email = "test1@gmail.com", password = "test1234" });
            var response = await _apiClient.PostAsync(@"/api/users/register", payload);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task users_controller_login_should_return_json_web_token()
        {
            var payload = GetPayload(new { email = "test1@gmail.com", password = "test1234" });
            var response = await _apiClient.PostAsync("/api/users/login", payload);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<JsonWebToken>(content);

            jwt.Should().NotBeNull();
            jwt.Token.Should().NotBeEmpty();
            jwt.Expiration.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task users_controller_get_should_return_user_details_view_model()
        {
            var payload = GetPayload(new { email = "test1@gmail.com", password = "test1234" });
            var loginResponse = await _apiClient.PostAsync("/api/users/login", payload);
            loginResponse.EnsureSuccessStatusCode();

            var content = await loginResponse.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<JsonWebToken>(content);

            _apiClient.DefaultRequestHeaders.Add("Authorization","Bearer " + jwt.Token); 
            var userDetailsResponse = await _apiClient.GetAsync("/api/users/me");
            userDetailsResponse.EnsureSuccessStatusCode();

            var userDetailsContent = await userDetailsResponse.Content.ReadAsStringAsync();
            var userDetails = JsonConvert.DeserializeObject<UserDetailsViewModel>(userDetailsContent);

            userDetails.Should().NotBeNull();
            userDetails.Id.Should().BeGreaterThan(0);
            userDetails.Email.Should().NotBeEmpty();
            userDetails.Email.Should().Be("test1@gmail.com");
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
