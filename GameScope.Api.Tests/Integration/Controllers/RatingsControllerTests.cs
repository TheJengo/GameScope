using FluentAssertions;
using GameScope.Application.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GameScope.Api.Tests.Integration.Controllers
{
    public class RatingsControllerTests : ControllerTestsBase
    {
        public RatingsControllerTests() : base()
        {

        }

        [Fact]
        public async Task ratings_controller_get_should_return_rating_list_view_model()
        {
            await PrepareForAuthorizedAction();
            var response = await ApiClient.GetAsync("/api/ratings");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var gameListViewModels = JsonConvert.DeserializeObject<List<RateListViewModel>>(content);
            gameListViewModels.Should().HaveCountGreaterOrEqualTo(0);
        }

        [Fact]
        public async Task ratings_controller_post_should_return_accepted()
        {
            await PrepareForAuthorizedAction();
            var payload = GetPayload(new { gameId = 1, value = 7 });
            var response = await ApiClient.PostAsync("/api/ratings", payload);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task ratings_controller_put_should_return_accepted()
        {
            await PrepareForAuthorizedAction();
            var payload = GetPayload(new { value = 6 });
            var response = await ApiClient.PutAsync("/api/ratings/3", payload);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task games_controller_delete_should_return_accepted()
        {
            await PrepareForAuthorizedAction();
            var response = await ApiClient.DeleteAsync("/api/ratings/3");
            response.EnsureSuccessStatusCode();
        }
    }
}
