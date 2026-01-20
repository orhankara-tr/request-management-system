using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Web.Mvc;

namespace RequestManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // Manager'ları tanımlıyoruz
        RequestManager requestManager = new RequestManager();
        UserManager userManager = new UserManager();

        public ActionResult Index()
        {
            int roleId = Session["RoleId"] != null ? Convert.ToInt32(Session["RoleId"]) : 0;
            int userId = Session["UserId"] != null ? Convert.ToInt32(Session["UserId"]) : 0;

            ViewBag.ToplamTalepSayisi = requestManager.GetTotalCount();

            if (roleId == (int)UserRole.Yonetici)
            {
                ViewBag.OnayBekleyenSayi = requestManager.GetPendingCount(); // Status: 
            }

            if (roleId == (int)UserRole.Kullanici)
            {
                ViewBag.TaleplerimSayi = requestManager.GetUserRequestCount(userId);
                ViewBag.BekleyenIslerSayi = requestManager.GetDraftCount(userId);   // Status: Taslak
            }

            if (roleId == (int)UserRole.Admin)
            {
                ViewBag.ToplamKullaniciSayi = userManager.List().Count;
                ViewBag.OnayBekleyenSayi = requestManager.GetPendingCount();
            }

            return View();
        }
    }
}