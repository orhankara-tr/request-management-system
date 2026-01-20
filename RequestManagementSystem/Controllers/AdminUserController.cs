using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using PagedList.Mvc;

namespace RequestManagementSystem.Controllers
{
    [Authorize]
    public class AdminUserController : Controller
    {
        UserManager userManager = new UserManager(new EFUserDal());
        // GET: AdminUser


        public ActionResult Index(int page = 1)
        {
            var userValues = userManager.List();
            return View(userValues.ToPagedList(page, 5));
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

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            var userValue = userManager.GetById(id);

            int currentUserId = Convert.ToInt32(Session["UserId"]);
            ViewBag.IsSelfEdit = (id == currentUserId);

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
                    userValue.RoleId = user.RoleId;
                    userValue.IsActive = user.IsActive;
                    if (!string.IsNullOrEmpty(user.PasswordHash))
                    {
                        userValue.PasswordHash = user.PasswordHash;
                    }
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

        public ActionResult DeleteUser(int id)
        {
            int currentUserId = Convert.ToInt32(Session["UserId"]);

            if (id == currentUserId)
            {
                TempData["Error"] = "Hesabınızı silemezsiniz!";
                return RedirectToAction("Index");
            }

            var userToDelete = userManager.GetById(id);
            if (userToDelete != null)
            {
                if (userToDelete.RoleId == UserRole.Admin)
                {
                    int adminCount = userManager.List().Count(x => x.RoleId == UserRole.Admin && x.IsActive == true);
                    if (adminCount <= 1)
                    {
                        TempData["Error"] = "En az bir aktif Admin olmalıdır!";
                        return RedirectToAction("Index");
                    }
                }

                userManager.Delete(userToDelete);
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Kullanıcı bulunamadı!";
            return RedirectToAction("Index");
        }

        public ActionResult ChangeStatus(int id)
        {
            int currentUserId = Convert.ToInt32(Session["UserId"]);

            if (id == currentUserId)
            {
                TempData["Error"] = "Durumunuzu değiştiremezsiniz!";
                return RedirectToAction("Index");
            }

            var userValue = userManager.GetById(id);
            if (userValue != null)
            {
                if (userValue.RoleId == UserRole.Admin && userValue.IsActive == true)
                {
                    int activeAdminCount = userManager.List().Count(x => x.RoleId == UserRole.Admin && x.IsActive == true);
                    if (activeAdminCount <= 1)
                    {
                        TempData["Error"] = "Aktif yöneticiyi pasif yapamazsınız!";
                        return RedirectToAction("Index");
                    }
                }

                userValue.IsActive = !userValue.IsActive;
                userManager.Update(userValue);
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Kullanıcı bulunamadı!";
            return RedirectToAction("Index");
        }

    }
}