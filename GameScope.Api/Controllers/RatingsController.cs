using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameScope.Api.Controllers
{
    /// <summary>
    /// api/ratings controller manages http requests for ratings entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ratings = _ratingService.GetAll();

            return StatusCode(StatusCodes.Status200OK, ratings);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody] RatingAddViewModel ratingAddViewModel)
        {
            ratingAddViewModel.UserId = Convert.ToInt32(User.Identity.Name);
            _ratingService.Add(ratingAddViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{gameId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Put([FromBody] RatingUpdateViewModel ratingUpdateViewModel, int gameId)
        {
            ratingUpdateViewModel.GameId = gameId;
            ratingUpdateViewModel.UserId = Convert.ToInt32(User.Identity.Name);
            _ratingService.Update(ratingUpdateViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{gameId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete(int gameId)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            _ratingService.Delete(userId, gameId);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
