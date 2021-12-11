using Core.Constants;
using Core.Utilities.Results.Abstract;
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
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var getAllResult = _postService.GetAll();
            if (!getAllResult.Success)
            {
                return BadRequest(getAllResult);
            }

            return Ok(getAllResult);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetPostsByUserId([FromRoute] int userId)
        {
            var result = _postService.GetPostByUserId(userId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{postId}")]
        public IActionResult GetPostsById([FromRoute] int postId)
        {
            var result = _postService.GetPostById(postId);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreatePost([FromBody] PostForAddDto postForAddDto)
        {
            if(!CheckUserIsLogin(postForAddDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var post = new Post { 
                Date = postForAddDto.CreateDate,
                Text = postForAddDto.Text,
                UserId = postForAddDto.UserId
            };

            var addedResult = _postService.AddPost(post);

            return Ok(addedResult);
        }

        

        private bool CheckUserIsLogin(int userId)
        {
            return userId == int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
