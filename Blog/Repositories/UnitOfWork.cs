using Blog.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public class UnitOfWork
    {
        private UserRepository _userRepo;
        private PostRepository _postRepo;
        private BlogDbContext dbContext;

        public UnitOfWork(BlogDbContext db)
        {
            dbContext = db;
           
        }

        public PostRepository PostRepo
        {
            get
            {
                if (_postRepo == null)
                {
                    _postRepo = new PostRepository(dbContext);
                }
                return _postRepo;
            }
        }

        public UserRepository UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepository(dbContext);
                }
                return _userRepo;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
