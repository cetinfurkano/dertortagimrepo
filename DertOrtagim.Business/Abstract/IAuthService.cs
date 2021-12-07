﻿using Core.Entities.SecurityModels;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.JWT;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DertOrtagim.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email, string userName);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
