using Core.Utilities.Results.Abstract;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;

namespace DertOrtagim.Business.Abstract
{
    public interface ILikeService
    {
        IResult AddLike(Like like);
        IResult RemoveLike(Like like);
        IDataResult<int> GetLikeCountByPostId(int postId);
        IDataResult<LikeForReturnDto> GetLikedUsersByPostId(int postId);
    }
}
