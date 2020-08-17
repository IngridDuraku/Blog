using Blog.Models;
using Blog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public class UserRepository
    {
        private BlogDbContext dbContext;
        public UserRepository(BlogDbContext db)
        {
            dbContext = db;
        }

        public User GetUserByName(string username)
        {
            return dbContext.Users.Where(x => x.Username == username).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            user.Password = Hash(user.Password);
            dbContext.Users.Add(user);
        }

        private string Hash(string password)
        {
            return Convert.ToBase64String(
                    System.Security.Cryptography.SHA256.Create()
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
                     );
        }

        public bool Authenticate(LoginViewModel model)
        {
            User usr = dbContext.Users.Where(x => x.Username == model.Username && x.Password == Hash(model.Password)).FirstOrDefault();
            if(usr == null)
            {
                return false;
            }
            return true;
        }
    }
}
