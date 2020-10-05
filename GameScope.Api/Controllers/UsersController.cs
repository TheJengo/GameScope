using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameScope.Application.Interfaces;
using GameScope.Application.ViewModels;
using GameScope.Infra.Common.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameScope.Api.Controllers
{
    /// <summary>
    /// api/users controller manages http requests for user entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(UserDetailsViewModel), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Get()
        {
            var user = _userService.GetById(Convert.ToInt32(User.Identity.Name));

            return StatusCode(StatusCodes.Status200OK, user);
        }

        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Register([FromBody] UserRegisterViewModel userRegisterViewModel) 
        {
            _userService.Register(userRegisterViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(JsonWebToken), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 500)]
        public IActionResult Login([FromBody] UserRegisterViewModel userRegisterViewModel)
        {
            var token = _userService.Login(userRegisterViewModel.Email, userRegisterViewModel.Password);

            return StatusCode(StatusCodes.Status200OK, token);
        }
    }
}
