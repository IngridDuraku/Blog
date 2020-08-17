using Blog.Models;
using Blog.ViewModels;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    public class PostRepository
    {
        private BlogDbContext dbContext;

        public PostRepository(BlogDbContext db)
        {
            dbContext = db;
        }

        public List<CategoryViewModel> GetCategories()
        {
            return (from categories in dbContext.Categories
                   select new CategoryViewModel 
                   { CategoryID = categories.CategoryId, Name = categories.Name }).ToList();
        }

        public void AddPost(Post post)
        {
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
        }
        public void SaveImage(Image image)
        {
            dbContext.Images.Add(image);
        }

        public Post GetPostById(int id)
        {
            return dbContext.Posts.Where(x => x.PostId == id).FirstOrDefault();
        }

        public Image GetImage(int postId)
        {
            return dbContext.Images.Where(x => x.PostId == postId).FirstOrDefault();
        }

        public void AddComment(Comment comment)
        {
            dbContext.Comments.Add(comment);
        }

        public List<CommentViewModel> GetComments(int postId)
        {
            return (from post in dbContext.Posts
                    join comments in dbContext.Comments on post.PostId equals comments.PostId
                    join usr in dbContext.Users on comments.UserId equals usr.UserId
                    where
                    post.PostId == postId
                    select new CommentViewModel
                    {
                        Text = comments.Text,
                        PostId = post.PostId,
                        UserId = comments.UserId,
                        Username = usr.Username
                    }).ToList();
        }

        public List<Post> GetPosts()
        {
            return dbContext.Posts.OrderByDescending(x => x.PostId).ToList();
        }

        public List<Post> GetPostsByCategory(int categoryID)
        {
            return (from post in dbContext.Posts
                    join PostCategory in dbContext.Post_Categories on post.PostId equals PostCategory.PostId
                    where PostCategory.CategoryId == categoryID
                    select post).OrderByDescending(x=>x.PostId).ToList();
        }

        public List<Post> SearchPosts(string text)
        {
            return dbContext.Posts.Where(x => x.Title.ToLower().StartsWith(text.ToLower())).OrderByDescending(x => x.PostId).ToList();
        }

        public void AddPostCategory(int CategoryID,int PostID)
        {
            dbContext.Post_Categories.Add(new Post_Category { CategoryId = CategoryID, PostId = PostID });
        }
    }
}
