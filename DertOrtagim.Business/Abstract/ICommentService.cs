using Core.Utilities.Results.Abstract;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Business.Abstract
{
    public interface ICommentService
    {
        IResult AddComment(Comment comment);
        IResult RemoveComment(int commentId, int userId);
        IDataResult<List<CommentForReturnDto>> GetCommentsByPostId(int postId);
        IDataResult<int> GetCommentCountByPostId(int postId);
    }
}
