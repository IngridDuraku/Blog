using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Repositories;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class Login : Controller
    {
        private BlogDbContext dbContext;
        private UnitOfWork _unitOfWork;
        public Login(BlogDbContext db)
        {
            dbContext = db;
            _unitOfWork = new UnitOfWork(dbContext);
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_unitOfWork.UserRepo.Authenticate(model))
                {
                    // Nis Sesioni
                    User usr = _unitOfWork.UserRepo.GetUserByName(model.Username);
                    HttpContext.Session.SetInt32("UserID", usr.UserId);
                    HttpContext.Session.SetString("Username", usr.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrMsg = "Fjalëkalim ose emër përdoruesi i gabuar!";
                }
            }
            
            return View("Index", model);
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
