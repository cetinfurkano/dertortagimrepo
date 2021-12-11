using Core.Constants;
using DertOrtagim.Business.Abstract;
using DertOrtagim.Entities.DBModels;
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
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }


        [HttpGet("post/{postId}")]
        public IActionResult GetLikesByPostId([FromRoute] int postId)
        {
            var likeCountResult = _likeService.GetLikeCountByPostId(postId);
            if (!likeCountResult.Success)
            {
                return BadRequest(likeCountResult.Message);
            }

            return Ok(likeCountResult);
        }
        
        
        [HttpPost]
        [Authorize]
        public IActionResult Like([FromBody] LikeForAddDto likeForAddDto)
        {
            if (!CheckUserIsLogin(likeForAddDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var like = new Like {
                PostId = likeForAddDto.PostId,
                UserId = likeForAddDto.UserId
            };

           var result =  _likeService.AddLike(like);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        
        [HttpDelete]
        [Authorize]
        public IActionResult UndoLike([FromBody] LikeForAddDto likeForAddDto)
        {
            if (!CheckUserIsLogin(likeForAddDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var like = new Like
            {
                PostId = likeForAddDto.PostId,
                UserId = likeForAddDto.UserId
            };

            var result = _likeService.RemoveLike(like);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
           
            return Ok(result);


        }

        private bool CheckUserIsLogin(int userId)
        {
            return userId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

    }
}
