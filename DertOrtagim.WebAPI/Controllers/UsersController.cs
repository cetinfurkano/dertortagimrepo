using Core.Constants;
using DertOrtagim.Business.Abstract;
using DertOrtagim.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DertOrtagim.WebAPI.Controllers
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
          
        [Authorize]
        [HttpPut]
        public IActionResult UpdateProfile([FromBody] UserForUpdateDto userForUpdateDto)
        {
            if (!CheckUserIsLogin(userForUpdateDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var result = _userService.UpdateUser(userForUpdateDto);
            
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [Authorize]
        [HttpPut("password")]
        public IActionResult UpdatePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            if (!CheckUserIsLogin(changePasswordDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var result = _userService.ChangePassword(changePasswordDto.Password, changePasswordDto.UserId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        private bool CheckUserIsLogin(int userId)
        {
            return userId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}
