using Core.DataAccess.EntityFramework;
using DertOrtagim.DataAccess.Abstract;
using DertOrtagim.DataAccess.Concrete.EntityFramework.Contexts;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;


namespace DertOrtagim.DataAccess.Concrete.EntityFramework
{
    public class EfCommentDal : EfEntityRepositoryBase<Comment,DertOrtagimDBContext>, ICommentDal
    {
        public List<CommentForReturnDto> GetAllCommentsByPostId(int postId)
        {
            using (var context = new DertOrtagimDBContext())
            {
                var result = new List<CommentForReturnDto>();

                using (var connection = context.Database.GetDbConnection())
                {
                    connection.Open();

                    var command = connection.CreateCommand();

                    command.CommandText = "[GetCommentsByPostId]";
                   
                    var parameter = new SqlParameter("PostId", System.Data.SqlDbType.Int);
                    parameter.Value = postId;
                    
                    command.Parameters.Add(parameter);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var commentForReturn = new CommentForReturnDto
                        {
                            CommentId = Convert.ToInt32(reader["CommentId"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            PostId = Convert.ToInt32(reader["PostId"]),
                            Text = reader["Text"].ToString(),
                            User = new UserForReturnDto
                            {
                                EMail = reader["Email"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                UserId = Convert.ToInt32(reader["UserId"])
                            }
                        };

                        result.Add(commentForReturn);
                    }
                }

               return result;
                   
            }
        }
    }
}
