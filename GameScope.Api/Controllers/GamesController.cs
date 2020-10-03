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
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var games = _gameService.GetAll();

            return StatusCode(StatusCodes.Status200OK, games);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GameCreateViewModel gameCreateViewModel)
        {
            gameCreateViewModel.UserId = Convert.ToInt32(User.Identity.Name);
            _gameService.Add(gameCreateViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] GameUpdateViewModel gameUpdateViewModel, int id)
        {
            gameUpdateViewModel.Id = id;
            gameUpdateViewModel.UserId = Convert.ToInt32(User.Identity.Name);
            _gameService.Update(gameUpdateViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = Convert.ToInt32(User.Identity.Name);
            _gameService.Delete(id, userId);

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
