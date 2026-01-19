using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FluentValidation.Results;
namespace RequestManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private UserManager userManager = new UserManager();

        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserList()
        {
            var uservalues = userManager.List();
            return View(uservalues);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            UserValidator userValidator = new UserValidator();
            var results = userValidator.Validate(user);
            if (results.IsValid)
            {
                userManager.Add(user);
                return RedirectToAction("GetUserList");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}