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

namespace GameScope.Api.Tests.Integration.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected TestServer ApiServer { get; private set; }
        protected HttpClient ApiClient { get; private set; }

        protected ControllerTestsBase()
        {
            ApiServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<GameScope.Api.Startup>());
            ApiClient = ApiServer.CreateClient();
        }

        protected async Task PrepareForAuthorizedAction()
        {
            var payload = GetPayload(new { email = "test1@gmail.com", password = "test1234" });
            var loginResponse = await ApiClient.PostAsync("/api/users/login", payload);
            loginResponse.EnsureSuccessStatusCode();

            var content = await loginResponse.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<JsonWebToken>(content);

            ApiClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwt.Token);
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
