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

        [HttpPost]
        public IActionResult Post([FromBody] RatingAddViewModel ratingAddViewModel)
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{gameId}")]
        public IActionResult Put([FromBody] RatingUpdateViewModel ratingUpdateViewModel, int gameId)
        {
            ratingUpdateViewModel.GameId = gameId;

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}"]
        public IActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
