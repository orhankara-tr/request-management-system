using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RequestHistoryManager : IRequestHistoryService
    {

        private readonly IRequestHistoryDal _historyDal;

        public RequestHistoryManager(IRequestHistoryDal historyDal)
        {
            _historyDal = historyDal;
        }
        public List<RequestHistory> GetByRequestId(int requestId)
        {
            return _historyDal.List(x => x.RequestId == requestId)
                              .OrderByDescending(x => x.CreatedDate)
                              .ToList();
        }

        public void Log(int requestId, int userId, string action, string fieldName = null, string oldValue = null, string newValue = null, string notes = null)
        {
            var history = new RequestHistory
            {
                RequestId = requestId,
                UserId = userId,
                Action = action,
                FieldName = fieldName,
                OldValue = oldValue,
                NewValue = newValue,
                Notes = notes,
                CreatedDate = DateTime.Now
            };

            _historyDal.Insert(history);
        }
    }
}
