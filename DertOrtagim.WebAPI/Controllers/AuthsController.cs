using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Hashing;
using DertOrtagim.Business.Abstract;
using DertOrtagim.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DertOrtagim.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public AuthsController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userLoginResult = _authService.Login(userForLoginDto);
            if (!userLoginResult.Success)
            {
                return BadRequest(userLoginResult.Message);
            }

            var tokenResult = _authService.CreateAccessToken(userLoginResult.Data);
            if (!tokenResult.Success)
            {
                return BadRequest(tokenResult.Message);
            }

            return Ok(tokenResult.Data);

        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExistsResult = _authService.UserExists(userForRegisterDto.EMail, userForRegisterDto.UserName);
            if (!userExistsResult.Success)
            {
                return BadRequest(userExistsResult.Message);
            }

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            if (!registerResult.Success)
            {
                return BadRequest(registerResult.Message);
            }

            return Ok();
        }
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("TEST");
        }
    }
}
