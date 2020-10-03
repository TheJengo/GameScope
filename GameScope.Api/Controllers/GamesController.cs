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
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GameCreateViewModel gameCreateViewModel)
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] GameUpdateViewModel gameUpdateViewModel, int  id)
        {
            gameUpdateViewModel.Id = id;

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("{id}"]
        public IActionResult Delete(int id)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
