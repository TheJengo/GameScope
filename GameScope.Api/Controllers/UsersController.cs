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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("me")]
        public IActionResult Get()
        {
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterViewModel userRegisterViewModel) 
        {
            _userService.Register(userRegisterViewModel);

            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserRegisterViewModel userRegisterViewModel)
        {

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
