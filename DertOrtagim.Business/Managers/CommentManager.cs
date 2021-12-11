using Core.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DertOrtagim.Business.Abstract;
using DertOrtagim.DataAccess.Abstract;
using DertOrtagim.Entities.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Utilities.Business;
using DertOrtagim.Entities.DTOs;

namespace DertOrtagim.Business.Managers
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public CommentManager(ICommentDal commentDal, IUserService userService, IPostService postService)
        {
            _commentDal = commentDal;
            _postService = postService;
            _userService = userService;
        }

        public IResult AddComment(Comment comment)
        {
            var businessRules = BusinessRules.Run(CheckUserExists(comment.UserId), CheckPostExists(comment.PostId));
            if (businessRules != null)
            {
                return businessRules;
            }
           

            _commentDal.Add(comment);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<int> GetCommentCountByPostId(int postId)
        {
            var businessRules = BusinessRules.Run(CheckPostExists(postId));
            if (businessRules != null)
            {
                return (IDataResult<int>)businessRules;
            }

            var count = _commentDal.GetAll(c => c.PostId == postId && !c.IsDeleted).Count;
            return new SuccessDataResult<int>(count, Messages.Success);
        }

        public IDataResult<List<CommentForReturnDto>> GetCommentsByPostId(int postId)
        {
            var businessRules = BusinessRules.Run(CheckPostExists(postId));
            if (businessRules != null)
            {
                return (IDataResult<List<CommentForReturnDto>>)businessRules;
            }

            var comments = _commentDal.GetAllCommentsByPostId(postId);
            return new SuccessDataResult<List<CommentForReturnDto>>(comments, Messages.Success);
        }

        public IResult RemoveComment(int commentId, int userId)
        {
            var comment = _commentDal.Get(f => !f.IsDeleted && f.Id == commentId && f.UserId == userId);

            var businessRules = BusinessRules.Run(CheckCommentExists(comment));

            if (businessRules != null)
            {
                return businessRules;
            }

            comment.IsDeleted = true;

            _commentDal.Update(comment);

            return new SuccessResult(Messages.Success);
        }

        private IResult CheckCommentExists(Comment comment)
        {
            if (comment == null)
            {
                return new ErrorResult(Messages.NoDataFoundError);
            }
            return new SuccessResult(Messages.Success);
        }
        private IResult CheckPostExists(int postId)
        {
            var post = _postService.GetPostById(postId);
            if (!post.Success)
            {
                return new ErrorResult(post.Message);
            }

            return new SuccessResult(Messages.Success);
        }
        private IResult CheckUserExists(int userId)
        {
            var user = _userService.GetUserById(userId);
            if (user == null)
            {
                return new ErrorResult(Messages.ThereIsNoSuchUser);
            }
            return new SuccessResult(Messages.Success);
        }

    }
}
