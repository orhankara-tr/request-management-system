using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Linq;
using System.Web.Mvc;

namespace RequestManagementSystem.Controllers
{
    [Authorize]
    public class RequestHistoryController : Controller
    {
        private readonly IRequestHistoryDal _historyDal = new EFRequestHistoryDal();

        public ActionResult Index(int? requestId)
        {
            if (Session["RoleId"] == null || Session["UserId"] == null)
                return RedirectToAction("Index", "Login");

            var role = (UserRole)Convert.ToInt32(Session["RoleId"]);

            if (role != UserRole.Yonetici && role != UserRole.Admin)
                return new HttpStatusCodeResult(403);

            var list = _historyDal.List();

            if (requestId.HasValue)
                list = list.Where(x => x.RequestId == requestId.Value).ToList();

            list = list.OrderByDescending(x => x.CreatedDate).ToList();

            return View(list);
        }
    }
}