using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RequestManager : IRequestService
    {
        private IRequestDal _requestDal;
        private IRequestHistoryDal _requestHistoryDal;

        public RequestManager()
        {
            _requestDal = new EFRequestDal();
            _requestHistoryDal = new EFRequestHistoryDal();

        }
        public RequestManager(IRequestDal requestDal)
        {
            _requestDal = requestDal;
            _requestHistoryDal = new EFRequestHistoryDal(); 

        }

        public void Add(Request request)
        {
            request.CreatedDate = DateTime.Now;
            _requestDal.Insert(request);

            request.RequestNo = $"TYS-{DateTime.Now.Year}-{request.RequestId.ToString().PadLeft(8, '0')}";
            _requestDal.Update(request);

            LogHistory(
                requestId: request.RequestId,
                userId: request.CreatedByUserId,
                action: "Oluşturuldu",
                notes: "Talep oluşturuldu."
            );
        }

        public void Update(Request request)
        {
            var existing = _requestDal.Get(x => x.RequestId == request.RequestId);
            if (existing == null) return;

            if (existing.StatusId == RequestStatus.Onaylandi)
                throw new InvalidOperationException("Onaylanan talep güncellenemez.");

            _requestDal.Update(request);
        }

        public Request GetById(int id)
        {
            return _requestDal.Get(x => x.RequestId == id);
        }

        public List<Request> List()
        {
            return _requestDal.List();
        }


        public List<Request> GetAll(Expression<Func<Request, bool>> filter)
        {
            return _requestDal.List(filter);
        }


        public List<Request> GetUserRequests(int userId)
        {
            return _requestDal.List(x => x.CreatedByUserId == userId);
        }

        public List<Request> GetManagerRequests()
        {
            throw new NotImplementedException();
        }

        public List<Request> GetPendingRequests()
        {
            return _requestDal.List(x => x.StatusId == RequestStatus.OnayBekliyor);
        }

        public bool Approve(int requestId, int managerId, string note = null)
        {
            var request = _requestDal.Get(x => x.RequestId == requestId);

            if (request == null || request.StatusId != RequestStatus.OnayBekliyor)
                return false;

            var oldStatus = request.StatusId; 

            request.StatusId = RequestStatus.Onaylandi;
            request.ProcessedByUserId = managerId;
            request.ProcessedDate = DateTime.Now;
            request.ApprovalNotes = note;

            _requestDal.Update(request);

            // history yaz
            LogHistory(
                requestId: request.RequestId,
                userId: managerId,
                action: "Onaylandı",
                fieldName: "Status",
                oldValue: oldStatus.ToString(),
                newValue: request.StatusId.ToString(),
                notes: note
            );

            return true;
        }
        public bool Reject(int requestId, int managerId, string reason)
        {
            var request = _requestDal.Get(x => x.RequestId == requestId);

            if (request == null || request.StatusId != RequestStatus.OnayBekliyor)
                return false;

            var oldStatus = request.StatusId;

            request.StatusId = RequestStatus.Reddedildi;
            request.ProcessedByUserId = managerId;
            request.ProcessedDate = DateTime.Now;
            request.ApprovalNotes = reason;

            _requestDal.Update(request);

            LogHistory(
                requestId: request.RequestId,
                userId: managerId,
                action: "Reddedildi",
                fieldName: "Status",
                oldValue: oldStatus.ToString(),
                newValue: request.StatusId.ToString(),
                notes: reason
            );

            return true;
        }
        public bool Submit(int requestId, int userId)
        {
            var request = _requestDal.Get(x => x.RequestId == requestId);

            if (request == null || request.CreatedByUserId != userId || request.StatusId != RequestStatus.Taslak)
                return false;

            var oldStatus = request.StatusId;

            request.StatusId = RequestStatus.OnayBekliyor;
            request.UpdatedDate = DateTime.Now;

            _requestDal.Update(request);

            LogHistory(
                requestId: request.RequestId,
                userId: userId,
                action: "Durumu Değiştirildi",
                fieldName: "Status",
                oldValue: oldStatus.ToString(),
                newValue: request.StatusId.ToString()
            );

            return true;
        }

        public List<RequestHistory> GetRequestHistory(int requestId)
        {
            return _requestHistoryDal.List(x => x.RequestId == requestId)
                                     .OrderByDescending(x => x.CreatedDate)
                                     .ToList();
        }

        public int GetTotalCount()
        {
            return _requestDal.Query().Count();
        }

        public int GetPendingCount()
        {
            return _requestDal.Query()
                .Count(x => x.StatusId == RequestStatus.OnayBekliyor);
        }

        public int GetDraftCount(int userId)
        {
            var query = _requestDal.Query().Where(x => x.StatusId == RequestStatus.Taslak);
            if (userId > 0)
            {
                query = query.Where(x => x.CreatedByUserId == userId);
            }

            return query.Count();
        }

        public int GetUserRequestCount(int userId)
        {
            return _requestDal.Query()
                .Count(x => x.CreatedByUserId == userId);
        }

        public IQueryable<Request> Query()
        {
            return _requestDal.Query();
        }

        public int GetCountByStatus(int statusId)
        {
            return _requestDal.Query()
                .Count(x => (int)x.StatusId == statusId);
        }

        public int GetCountByUserId(int userId)
        {
            return _requestDal.Query()
                .Count(x => x.CreatedByUserId == userId);
        }

        private void LogHistory(int requestId, int userId, string action,
                        string fieldName = null, string oldValue = null, string newValue = null, string notes = null)
        {
            _requestHistoryDal.Insert(new RequestHistory
            {
                RequestId = requestId,
                UserId = userId,
                Action = action,
                FieldName = fieldName,
                OldValue = oldValue,
                NewValue = newValue,
                Notes = notes,
                CreatedDate = DateTime.Now
            });
        }
    }
}
