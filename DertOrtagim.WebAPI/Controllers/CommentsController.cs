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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("post/{postId}")]
        public IActionResult GetCommentsByPostId([FromRoute]int postId)
        {
            var result = _commentService.GetCommentsByPostId(postId);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpPost]
        [Authorize]
        public IActionResult AddComment(CommentForAddDto commentForAddDto)
        {
            if (!CheckUserIsLogin(commentForAddDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var result =  _commentService.AddComment(new Comment { 
                Date = commentForAddDto.Date,
                PostId = commentForAddDto.PostId,
                Text = commentForAddDto.Text,
                UserId = commentForAddDto.UserId
            });

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpDelete]
        [Authorize]
        public IActionResult RemoveComment(CommentForRemoveDto commentForRemoveDto)
        {
            if (!CheckUserIsLogin(commentForRemoveDto.UserId))
            {
                return Unauthorized(Messages.AuthorizationDenied);
            }

            var result = _commentService.RemoveComment(commentForRemoveDto.CommentId, commentForRemoveDto.UserId);

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

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); 
        }
    }
}
