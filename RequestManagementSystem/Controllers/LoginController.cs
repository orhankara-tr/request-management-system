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
    public class LoginController : Controller
    {
        // GET: Login
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
            if (loginUser != null)
            {
                FormsAuthentication.SetAuthCookie(loginUser.Username, false);
                Session["Username"] = loginUser.Username;
                Session["FullName"] = loginUser.FullName;
                Session["RoleId"] = loginUser.RoleId;
                return RedirectToAction("Index", "AdminUser");
            }
            else
            {
                ViewBag.ErrorMessage = "Geçersiz kullanıcı adı veya şifre.";
            }
            return View();
        }
    }
}