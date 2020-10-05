using FluentAssertions;
using GameScope.Application.ViewModels;
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
    public class GamesControllerTests : ControllerTestsBase
    {
        public GamesControllerTests() : base()
        {
        }

        [Fact]
        public async Task games_controller_get_should_return_games_list_view_model()
        {
            await PrepareForAuthorizedAction();
            var response = await ApiClient.GetAsync("/api/games");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var gameListViewModels = JsonConvert.DeserializeObject<List<GameListViewModel>>(content);
            gameListViewModels.Should().HaveCountGreaterOrEqualTo(0);
        }

        [Fact]
        public async Task games_controller_post_should_return_accepted()
        {
            await PrepareForAuthorizedAction();
            var payload = GetPayload(new { userId = 0, name = "Test Game", description = "test description for game", releaseDate = DateTime.UtcNow });
            var response = await ApiClient.PostAsync("/api/games", payload);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task games_controller_get_by_id_should_return_game_details_view_model()
        {
            await PrepareForAuthorizedAction();
            var response = await ApiClient.GetAsync("/api/games/3");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var gameDetails = JsonConvert.DeserializeObject<GameDetailsViewModel>(content);
            gameDetails.Id.Should().Be(3);
            gameDetails.UserId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task games_controller_put_should_return_accepted()
        {
            await PrepareForAuthorizedAction();
            var response = await ApiClient.GetAsync("/api/games/3");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var gameDetails = JsonConvert.DeserializeObject<GameDetailsViewModel>(content);

            gameDetails.Name = "putControllerGameTest";
            var payload = GetPayload(gameDetails);
            var putResponse = await ApiClient.PutAsync("/api/games/3", payload);
            putResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task games_controller_delete_should_return_accepted()
        {
            await PrepareForAuthorizedAction();
            var response = await ApiClient.DeleteAsync("/api/games/3");
            response.EnsureSuccessStatusCode();
        }
    }
}
