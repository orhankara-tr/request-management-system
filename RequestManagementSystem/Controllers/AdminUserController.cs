using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestManagementSystem.Controllers
{
    public class AdminUserController : Controller
    {
        UserManager userManager = new UserManager(new EFUserDal());
        // GET: AdminUser

        //[Authorize]
        public ActionResult Index()
        {
            var userValues = userManager.List();
            return View(userValues);
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
            var result = userValidator.Validate(user);
            if (result.IsValid)
            {
                userManager.Add(user);
                return RedirectToAction("Index", "AdminUser");
            }
            else
            {
                ModelState.Clear();
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(user);
        }

        public ActionResult DeleteUser(int id)
        {
            var userValue = userManager.GetById(id);
            userManager.Delete(userValue);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatus(int id)
        {
            var userValue = userManager.GetById(id);

            if (userValue.IsActive) { userValue.IsActive = false; }
            else { userValue.IsActive = true; }

            userManager.Update(userValue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditUser (int id)
        {
            var userValue = userManager.GetById(id);
            return View(userValue);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            UserValidator userValidator = new UserValidator();
            var result = userValidator.Validate(user);

            if (result.IsValid) 
            {
                var userValue = userManager.GetById(user.UserId);
                if (userValue != null)
                {
                    userValue.Username = user.Username;
                    userValue.FullName = user.FullName;
                    userValue.Email = user.Email;
                    userValue.PasswordHash = user.PasswordHash;
                    userValue.RoleId = user.RoleId;
                    userValue.IsActive = user.IsActive;

                    userManager.Update(userValue);
                    return RedirectToAction("Index", "AdminUser");
                }
            }
            else
            {
                ModelState.Clear();
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(user);
        }
    }
}