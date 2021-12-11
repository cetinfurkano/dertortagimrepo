using Core.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DertOrtagim.Business.Abstract;
using DertOrtagim.DataAccess.Abstract;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Utilities.Business;

namespace DertOrtagim.Business.Managers
{
    public class LikeManager : ILikeService
    {
        private ILikeDal _likeDal;

        public LikeManager(ILikeDal likeDal)
        {
            _likeDal = likeDal;
        }

        public IResult AddLike(Like like)
        {
            var businessRulesResult = BusinessRules.Run(CheckUserAlreadyLiked(like));

            if (businessRulesResult != null)
            {
                return businessRulesResult;
            }

            _likeDal.Add(like);
            return new SuccessResult(Messages.Success);
        }

        public IDataResult<int> GetLikeCountByPostId(int postId)
        {
            var count = _likeDal.GetAll(l => l.PostId == postId).Count;
            return new SuccessDataResult<int>(count, Messages.Success);
        }

        public IDataResult<LikeForReturnDto> GetLikedUsersByPostId(int postId)
        {
            var result = new LikeForReturnDto
            {
                PostId = postId,
                UserIds = _likeDal.GetAll(l => l.PostId == postId).Select(l => l.UserId).ToList()
            };
            return new SuccessDataResult<LikeForReturnDto>(result, Messages.Success);
        }

        public IResult RemoveLike(Like like)
        {
            var likeToBeDelete = _likeDal.Get(l => l.UserId == like.UserId && l.PostId == like.PostId);
            
            var businessRulesResult =  BusinessRules.Run(CheckPostLikeCountIsZero(like), CheckLikeIsNull(like));


            if (businessRulesResult != null)
            {
                return businessRulesResult;
            }


            _likeDal.Delete(likeToBeDelete);
            return new SuccessResult(Messages.Success);
        }


        private IResult CheckUserAlreadyLiked(Like like)
        {
            var result = _likeDal.Get(l => l.PostId == like.PostId && l.UserId == like.UserId);

            if (result != null)
            {
                return new ErrorResult(Messages.AlreadyLikedCurrentPost);
            }
            return new SuccessResult(Messages.Success);
        }

        private IResult CheckPostLikeCountIsZero(Like like)
        {
            var likeCount = _likeDal.GetAll(l => l.PostId == like.PostId);
            if (likeCount != null)
            {
                if (likeCount.Count <= 0)
                {
                    return new ErrorResult(Messages.ZeroLikesOnThePost);
                }
                return new SuccessResult(Messages.Success);

            }
            return new ErrorResult(Messages.NoDataFoundError);

        }

        private IResult CheckLikeIsNull(Like like)
        {
            if (like == null)
            {
                return new ErrorResult(Messages.NoDataFoundError);
            }
            return new SuccessResult(Messages.Success);

        }

    }
}
