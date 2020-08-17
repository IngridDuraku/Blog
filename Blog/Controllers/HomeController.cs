using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.Models;
using Microsoft.AspNetCore.Http;
using Blog.Repositories;
using Blog.ViewModels;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDbContext dbContext;
        private UnitOfWork _unitOfWork;
        public HomeController(BlogDbContext db)
        {
            dbContext = db;
            _unitOfWork = new UnitOfWork(dbContext);
        }

        public IActionResult Index(int id)
        {
            int userID;
            string username;
            try
            {
                userID = (int)HttpContext.Session.GetInt32("UserID");
                username = (string)HttpContext.Session.GetString("Username");
                ViewBag.Username = username;
            }
            catch (Exception e)
            {
                ViewBag.Username = null;
            }

            ViewBag.Categories = _unitOfWork.PostRepo.GetCategories();

            List<PostDetailViewModel> models = new List<PostDetailViewModel>();
            List<Post> posts;
            if (id == 0)
            {
                 posts = _unitOfWork.PostRepo.GetPosts();
            }
            else
            {
                posts = _unitOfWork.PostRepo.GetPostsByCategory(id);
            }
            foreach (var post in posts)
            {
                PostDetailViewModel model = new PostDetailViewModel
                {
                    Title = post.Title,
                    PostId = post.PostId,
                    Content = post.Content,
                    ImageName = _unitOfWork.PostRepo.GetImage(post.PostId).ImageName,
                    ImageUniqueName = _unitOfWork.PostRepo.GetImage(post.PostId).UniqueImgName
                };
                models.Add(model);
            };
            
            return View(models);
        }

        public IActionResult Create()
        {
            int userID;
            string username;
            try
            {
                userID = (int)HttpContext.Session.GetInt32("UserID");
                username = (string)HttpContext.Session.GetString("Username");
                ViewBag.Username = username;
            }catch(Exception e)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Categories = _unitOfWork.PostRepo.GetCategories();

            PostViewModel model = new PostViewModel();
            model.Categories = _unitOfWork.PostRepo.GetCategories();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(PostViewModel model)
        {
            string error = string.Empty;
            try
            {
                
                if(model.Title == null || model.Title.Trim() == string.Empty)
                {
                    error = "Ju lutem plotësoni titullin!";
                }
                else if (model.Content == null || model.Content.Trim() == string.Empty)
                {
                    error = "Ju lutem plotësoni përmbajtjen!";
                }
                else if(model.CategoryID == null || model.CategoryID.Count == 0)
                {
                    error = "Ju lutem zgjidhni të paktën një kategori!";
                }
                else if (model.Image == null)
                {
                   error = "Ju lutem ngarkoni imazhin!";
                    
                }

                if(error != string.Empty)
                {
                    return new JsonResult(new { success = false, message = error});
                }

                Post post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = (int)(int)HttpContext.Session.GetInt32("UserID")
                };
                _unitOfWork.PostRepo.AddPost(post);

                foreach(int categoryId in model.CategoryID)
                {
                    _unitOfWork.PostRepo.AddPostCategory(categoryId, post.PostId);
                }
                // Ruajtja e imazhit 
                string uniqueImgName = Convert.ToString(Guid.NewGuid()) + Path.GetExtension(model.Image.FileName);
                var filePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")).Root + $@"\{uniqueImgName}";
                
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    model.Image.CopyTo(fs);
                    fs.Flush();
                }
                Image img = new Image
                {
                    ImageName = model.Image.FileName,
                    ImagePath = filePath,
                    UniqueImgName = uniqueImgName,
                    PostId = post.PostId
                };

                _unitOfWork.PostRepo.SaveImage(img);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
               error = "Ka ndodhur një gabim në sistem, ju lutem provoni më vonë!";
            }
            return new JsonResult(new { success = false, message = error });
        }

        public IActionResult Details(int id)
        {
            Post post = _unitOfWork.PostRepo.GetPostById(id);
            Image img = _unitOfWork.PostRepo.GetImage(id);

            if(post == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string username;
            try
            {
                username = (string)HttpContext.Session.GetString("Username");
                ViewBag.Username = username;
            }
            catch (Exception e)
            {
                ViewBag.Username = null;
            }
            ViewBag.Categories = _unitOfWork.PostRepo.GetCategories();


            List<CommentViewModel> comments = _unitOfWork.PostRepo.GetComments(id);
            PostDetailViewModel model = new PostDetailViewModel
            {
                PostId = id,
                Content = post.Content,
                Title = post.Title,
                ImageName = img.ImageName,
                ImageUniqueName = img.UniqueImgName,
                Comments = comments
            };
            return View(model);
        }

        public IActionResult Comment(CommentViewModel model)
        {
            int userID;
            try
            {
                userID = (int)HttpContext.Session.GetInt32("UserID");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Login");
            }
            Comment commet = new Comment
            {
                PostId = model.PostId,
                Text = model.Text,
                UserId = userID
            };
            _unitOfWork.PostRepo.AddComment(commet);
            _unitOfWork.Save();
            return new JsonResult(new { success = true });
        }
        
        public IActionResult Search(string title)
        {
            int userID;
            string username;
            try
            {
                userID = (int)HttpContext.Session.GetInt32("UserID");
                username = (string)HttpContext.Session.GetString("Username");
                ViewBag.Username = username;
            }
            catch (Exception e)
            {
                ViewBag.Username = null;
            }

            ViewBag.Categories = _unitOfWork.PostRepo.GetCategories();

            List<PostDetailViewModel> models = new List<PostDetailViewModel>();
            List<Post> posts;
            if (title == null)
            {
                posts = _unitOfWork.PostRepo.SearchPosts("");
            }
            else
            {
                posts = _unitOfWork.PostRepo.SearchPosts(title);

            }

            foreach (var post in posts)
            {
                PostDetailViewModel model = new PostDetailViewModel
                {
                    Title = post.Title,
                    PostId = post.PostId,
                    Content = post.Content,
                    ImageName = _unitOfWork.PostRepo.GetImage(post.PostId).ImageName,
                    ImageUniqueName = _unitOfWork.PostRepo.GetImage(post.PostId).UniqueImgName
                };
                models.Add(model);
            };

            return View("Index", models);
        }
    }
}
