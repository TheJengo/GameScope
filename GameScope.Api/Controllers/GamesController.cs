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
    /// api/games controller manages http requests for game entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GameListViewModel>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            var games = _gameService.GetAll();

            return StatusCode(StatusCodes.Status200OK, games);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<GameListViewModel>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get(int id)
        {
            var game = _gameService.GetById(id);

            return StatusCode(StatusCodes.Status200OK, game);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Post([FromBody] GameCreateViewModel gameCreateViewModel)
        {
            gameCreateViewModel.UserId = Convert.ToInt32(User.Identity.Name);
            _gameService.Add(gameCreateViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Put([FromBody] GameUpdateViewModel gameUpdateViewModel, int id)
        {
            gameUpdateViewModel.Id = id;
            gameUpdateViewModel.UserId = Convert.ToInt32(User.Identity.Name);
            _gameService.Update(gameUpdateViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Delete(int id)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            _gameService.Delete(id, userId);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
