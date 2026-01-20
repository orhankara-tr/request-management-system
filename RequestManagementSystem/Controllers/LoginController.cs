using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace RequestManagementSystem.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User user)
        {
            Context content = new Context();
            var loginUser = content.Users.FirstOrDefault(u => u.Username == user.Username && u.PasswordHash == user.PasswordHash && u.IsActive);
            string sessionRole = Session["RoleName"]?.ToString();

            if (loginUser != null)
            {
                FormsAuthentication.SetAuthCookie(loginUser.Username, false);

                int userId = loginUser.UserId;
                string username = loginUser.Username;
                string fullName = loginUser.FullName;
                int roleId = (int)loginUser.RoleId; 

                Session["UserId"] = userId;
                Session["Username"] = username;
                Session["FullName"] = fullName;
                Session["RoleId"] = roleId;

                string roleName = "";
                switch (loginUser.RoleId) 
                {
                    case UserRole.Kullanici:
                        roleName = "Kullanıcı";
                        break;
                    case UserRole.Yonetici:
                        roleName = "Yönetici";
                        break;
                    case UserRole.Admin:
                        roleName = "Admin";
                        break;
                    default:
                        roleName = "Bilinmeyen";
                        break;
                }
                Session["RoleName"] = roleName;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}