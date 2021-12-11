using Core.Constants;
using Core.Entities.SecurityModels;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Hashing;
using DertOrtagim.Business.Abstract;
using DertOrtagim.DataAccess.Abstract;
using DertOrtagim.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Business.Managers
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(Messages.Success);
        }

        public User GetByMail(string email)
        {
            var result = _userDal.Get(u => u.EMail == email);
            return result;
        }

        public User GetByUserName(string userName)
        {
            var result = _userDal.Get(u => u.UserName == userName);
            return result;
        }

        public bool UserExists(string email, string userName)
        {
            var result = _userDal.Get(u =>u.UserName.Equals(userName) || u.EMail.Equals(email));
            
            return result != null;
        }

        public User GetUserById(int userId)
        {
            var result = _userDal.Get(u => u.Id == userId);
            return result;
        }

        public IDataResult<int> GetUserIdByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserForReturnDto> UpdateUser(UserForUpdateDto userForUpdateDto)
        {
            var user = _userDal.Get(u => u.Id == userForUpdateDto.UserId);

            user.FirstName = userForUpdateDto.FirstName;
            user.LastName = userForUpdateDto.LastName;
            user.EMail = userForUpdateDto.EMail;
            user.UserName = userForUpdateDto.UserName;

           var updatedUser =  _userDal.Update(user);

            var result = new UserForReturnDto
            {
                UserName = updatedUser.UserName,
                UserId = updatedUser.Id,
                EMail = updatedUser.EMail,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                ProfilePicture = updatedUser.ProfilePicture
            };


            return new SuccessDataResult<UserForReturnDto>(result, Messages.Success);
        }

        public IResult ChangePassword(string password, int userId)
        {
            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = _userDal.Get(u => u.Id == userId);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _userDal.Update(user);

            return new SuccessResult(Messages.Success);
        }

        public IDataResult<UserForReturnDto> ChangeProfilePicture(int userId, string picture)
        {
            var user = _userDal.Get(u => u.Id == userId);

            user.ProfilePicture = picture;

            var updatedUser = _userDal.Update(user);

            var result = new UserForReturnDto
            {
                ProfilePicture = updatedUser.ProfilePicture,
                EMail = updatedUser.EMail,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                UserId = updatedUser.Id,
                UserName = updatedUser.UserName
            };

            return new SuccessDataResult<UserForReturnDto>(result,Messages.Success);

        }
    }
}
