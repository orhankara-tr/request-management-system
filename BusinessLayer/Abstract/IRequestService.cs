using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRequestService
    {
        void Add(Request request);
        void Update(Request request);
        Request GetById(int id);
        List<Request> List();
        List<Request> GetAll(Expression<Func<Request, bool>> filter);


        // Kullanıcıya ait istekler
        List<Request> GetUserRequests(int userId);

        // Beklemede olan istekler
        List<Request> GetPendingRequests();

        // Yöneticinin onaylaması gereken istekler
        List<Request> GetManagerRequests();

        // Yönetici talepleri onaylama 
        bool Approve(int requestId, int managerId, string note = null);

        // Yönetici talepleri reddetme - not zorunlu
        bool Reject(int requestId, int managerId, string reason);

        // Kullanıcı talepleri gönderme - Taslaktan Onay Bekleme duruma geçer
        bool Submit(int requestId, int userId);


        // Talep geçmişini alma
        List<RequestHistory> GetRequestHistory(int requestId);

        int GetTotalCount(); // Tüm taleplerin sayısı

        int GetCountByStatus(int statusId); // Belirli bir durumdaki (Bekleyen, Onaylanan) taleplerin sayısı

        int GetCountByUserId(int userId); // Belirli bir kullanıcının kendi taleplerinin sayısı

        IQueryable<Request> Query();

        // Toplam sayıları almak için gerekli imzalar
        int GetPendingCount();

        // Taslak sayısını ister genel ister kullanıcı bazlı almak için default 0 parametreli
        int GetDraftCount(int userId = 0);

        // Kullanıcının toplam talebi
        int GetUserRequestCount(int userId);

    }
}
