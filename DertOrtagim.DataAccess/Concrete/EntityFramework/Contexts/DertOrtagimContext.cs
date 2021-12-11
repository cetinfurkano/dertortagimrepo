using Core.Entities.SecurityModels;
using DertOrtagim.Entities.DBModels;
using DertOrtagim.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Text;

namespace DertOrtagim.DataAccess.Concrete.EntityFramework.Contexts
{
    public class DertOrtagimDBContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=DERTORTAGIMDB;Trusted_Connection=false;User Id=furkan;Password=Start!2021");
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

         
    }
}
