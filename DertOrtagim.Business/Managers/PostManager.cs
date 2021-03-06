using Core.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DertOrtagim.Business.Abstract;
using DertOrtagim.DataAccess.Abstract;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Business.Managers
{
    public class PostManager : IPostService
    {
        private IPostDal _postDal;
        private ILikeService _likeService;
        public PostManager(IPostDal postDal, ILikeService likeService)
        {
            _postDal = postDal;
            _likeService = likeService;
            
        }

        public IResult AddPost(Post post)
        {
            _postDal.Add(post);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<PostForReturnDto> GetPostById(int postId)
        {
            var post = _postDal.Get(p => p.Id == postId && !p.IsDeleted);
            var businessRules = BusinessRules.Run(CheckPostExists(post));
            if (businessRules != null)
            {
                return (IDataResult<PostForReturnDto>)businessRules;
            }

            var result = new PostForReturnDto
            {
                Id = postId,
                //CommentCount = _commentService.GetCommentCountByPostId(postId).Data,
                LikeCount = _likeService.GetLikeCountByPostId(postId).Data,
                Date = post.Date,
                Text = post.Text,
                UserId = post.UserId,
                //ProfilePicture = _profilePictureService.GetProfilePictureByUserId(post.UserId).Data.Data
            };
            return new SuccessDataResult<PostForReturnDto>(result, Messages.Success);
        }

        public IDataResult<List<PostForReturnDto>> GetPostByUserId(int userId)
        {
            var posts = _postDal.GetAll(p => p.UserId == userId);
            var result = new List<PostForReturnDto>();
            //var profilePicture = _profilePictureService.GetProfilePictureByUserId(posts[0].UserId).Data.Data;
            foreach (var post in posts)
            {
                result.Add(new PostForReturnDto
                {
                    Id = post.Id,
                    //CommentCount = _commentService.GetCommentCountByPostId(post.Id).Data,
                    LikeCount = _likeService.GetLikeCountByPostId(post.Id).Data,
                    Date = post.Date,
                    Text = post.Text,
                    UserId = post.UserId,
                    //ProfilePicture = profilePicture
                });
            }
            return new SuccessDataResult<List<PostForReturnDto>>(result, Messages.Success);
        }

        public IResult RemovePost(int postId)
        {
            var post = _postDal.Get(p => p.Id == postId);
            post.IsDeleted = true;

            _postDal.Update(post);

            return new SuccessResult(Messages.Success);
        }

        public IDataResult<List<PostForReturnDto>> GetAll()
        {

            var posts = _postDal.GetAll(p => !p.IsDeleted);

            if (posts == null)
            {
                return new ErrorDataResult<List<PostForReturnDto>>(Messages.NoDataFoundError);
            }
            var result = new List<PostForReturnDto>();

            foreach (var post in posts)
            {
                result.Add(new PostForReturnDto
                {
                    Id = post.Id,
                    //CommentCount = _commentService.GetCommentCountByPostId(post.Id).Data,
                    LikeCount = _likeService.GetLikeCountByPostId(post.Id).Data,
                    Date = post.Date,
                    Text = post.Text,
                    UserId = post.UserId,
                    //ProfilePicture = profilePicture
                });
            }

            return new SuccessDataResult<List<PostForReturnDto>>(result, Messages.Success);
        }

        private IResult CheckPostExists(Post post)
        {
            if (post == null)
            {
                return new ErrorResult(Messages.NoDataFoundError);
            }
            return new SuccessResult(Messages.Success);
        }
    }
}
