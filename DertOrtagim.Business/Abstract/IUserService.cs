using Core.Entities.SecurityModels;
using Core.Utilities.Results.Abstract;
using DertOrtagim.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Business.Abstract
{
    public interface IUserService
    { 
        IResult Add(User user);
        User GetByMail(string email);
        User GetUserById(int userId);
        User GetByUserName(string userName);
        IDataResult<int> GetUserIdByUserName(string userName);
        bool UserExists(string email, string userName);
        IDataResult<UserForReturnDto> UpdateUser(UserForUpdateDto user);
        IResult ChangePassword(string password, int userId);

        IDataResult<UserForReturnDto> ChangeProfilePicture(int userId, string picture);


    }
}
