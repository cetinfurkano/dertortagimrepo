using Core.Entities.SecurityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Constants
{
    public static class Messages
    {
        public const string AuthorizationDenied = "You are not authorized";
        public const string Success = "The process successfully completed.";
        public const string ThereIsNoSuchUser = "There is no such user";
        public const string TokenWasCreated = "Token was created.";
        public const string UserHasAlreadyExists = "This user has already exists.";
        public const string WrongPassword = "Password or username is wrong.";
        public const string InternalServerError = "Internal server error occured!";


        public const string NoDataFoundError = "No data found.";

        public const string AlreadyLikedCurrentPost = "This user already liked this post.";
        public const string ZeroLikesOnThePost = "0 like on the current post.";

    }
}
