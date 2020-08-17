using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.Repositories;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Blog.Controllers
{
    public class Registration : Controller
    {
        private BlogDbContext dbContext;
        private UnitOfWork _unitOfWork;
        public Registration(BlogDbContext db)
        {
            dbContext = db;
            _unitOfWork = new UnitOfWork(dbContext);
        }
        
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            // Check Username
            if (ModelState.IsValid)
            {
               if( _unitOfWork.UserRepo.GetUserByName(model.Username)!= null)
                {
                    ViewBag.ErrMsg = "Emri i përdoruesit nuk është unik!";
                    return View(model);
                }
               else if(model.Password != model.ConfirmPassword)
                {
                    // Check Paassword <-> Confirm Password
                    ViewBag.ErrMsg = "Fjalëkalimet e dhëna nuk janë të njëjta!";
                    return View(model);
                }
                User usr = new User
                {
                    Username = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password
                };

                _unitOfWork.UserRepo.AddUser(usr);

                try
                {
                    _unitOfWork.Save();
                }
                catch(SqlException e)
                {
                    ViewBag.ErrMsg = "Ka ndodhur një gabim në sistem! Ju lutem provoni më vonë!";
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
