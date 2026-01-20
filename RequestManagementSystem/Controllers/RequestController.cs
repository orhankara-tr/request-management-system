using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RequestManagementSystem.Controllers
{
    [Authorize]
    public class RequestController : Controller
    {
        RequestManager requestManager = new RequestManager(new EFRequestDal());

        // GET: Request
        public ActionResult MyRequests(DateTime? startDate, DateTime? endDate, int page = 1)
        {
            int userId = Convert.ToInt32(Session["UserId"]);

            var query = requestManager.Query()
                .Where(x => x.CreatedByUserId == userId);

            if (startDate.HasValue || endDate.HasValue)
            {
                page = 1;
            }

            if (startDate.HasValue)
            {
                DateTime start = startDate.Value.Date;
                query = query.Where(x => x.CreatedDate >= start);
            }

            if (endDate.HasValue)
            {
                DateTime end = endDate.Value.Date.AddDays(1);
                query = query.Where(x => x.CreatedDate < end);
            }

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            return View(
                query
                    .OrderByDescending(x => x.CreatedDate)
                    .ToPagedList(page, 10)
            );
        }

        public ActionResult Index(DateTime? startDate, DateTime? endDate, int page = 1)
        {
            if (startDate.HasValue || endDate.HasValue)
                page = 1;

            var query = requestManager.Query();

            if (startDate.HasValue)
            {
                DateTime start = startDate.Value;
                query = query.Where(x =>
                    DbFunctions.TruncateTime(x.CreatedDate) >= start);
            }

            if (endDate.HasValue)
            {
                DateTime end = endDate.Value;
                query = query.Where(x =>
                    DbFunctions.TruncateTime(x.CreatedDate) <= end);
            }

            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            var pagedList = query
                .OrderByDescending(x => x.CreatedDate)
                .ToPagedList(page, 10);

            ViewBag.TotalRecords = query.Count();
            ViewBag.PagedRecords = pagedList.Count;

            return View(pagedList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Request request)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            request.CreatedByUserId = userId;
            request.StatusId = RequestStatus.Taslak; 
            request.CreatedDate = DateTime.Now;
            RequestValidator requestValidator = new RequestValidator();
            var result = requestValidator.Validate(request);
            if (result.IsValid)
            {
                requestManager.Add(request);
                return RedirectToAction("MyRequests");
            }
            else
            {
                ModelState.Clear();
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View(request);
        }


        [HttpGet]
        public ActionResult Submit(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            requestManager.Submit(id, userId);
            return RedirectToAction("MyRequests");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var requestValue = requestManager.GetById(id);
            int userId = Convert.ToInt32(Session["UserId"]);

            if (requestValue == null || requestValue.CreatedByUserId != userId)
            {
                return RedirectToAction("MyRequests");
            }

            if (requestValue.StatusId != RequestStatus.Taslak && requestValue.StatusId != RequestStatus.Reddedildi)
            {
                return RedirectToAction("MyRequests");
            }

            return View(requestValue);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Request request)
        {
            var value = requestManager.GetById(request.RequestId);

            if (ModelState.IsValid)
            {
                value.Title = request.Title;
                value.Description = request.Description;

                if (value.StatusId == RequestStatus.Reddedildi)
                    value.StatusId = RequestStatus.Taslak;

                requestManager.Update(value);
                return RedirectToAction("MyRequests");
            }

            return View(request); 
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var request = requestManager.GetById(id);
            if (request == null)
            {
                return RedirectToAction("Index");
            }

            int currentUserId = Convert.ToInt32(Session["UserId"]);
            var roleId = Session["RoleId"] != null ? Convert.ToInt32(Session["RoleId"]) : 0;

            ViewBag.IsAdminOrManager = (roleId == (int)UserRole.Admin) || (roleId == (int)UserRole.Yonetici);

            if (roleId == (int)UserRole.Kullanici && request.CreatedByUserId != currentUserId)
            {
                return RedirectToAction("MyRequests");
            }

            return View(request);
        }


        [Authorize]
        [HttpGet]
        public ActionResult AdminRequestList(int page = 1)
        {
            var values = requestManager.GetPendingRequests().ToPagedList(page, 10);
            return View(values);
        }


        [HttpGet]
        public ActionResult Approve(int id)
        {
            int managerId = Convert.ToInt32(Session["UserId"]);
            string role = Session["UserRole"]?.ToString();

            bool result = requestManager.Approve(id, managerId, "Onaylandı.");

            if (role == "Yönetici")
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("MyRequests");
        }

        [HttpGet]
        public ActionResult Reject(int id, string reason)
        {
            int managerId = Convert.ToInt32(Session["UserId"]);
            requestManager.Reject(id, managerId, reason);
            return RedirectToAction("Index");
        }

    }
}