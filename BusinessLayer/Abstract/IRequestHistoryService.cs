using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRequestHistoryService
    {
        void Log(int requestId, int userId, string action,
         string fieldName = null, string oldValue = null, string newValue = null, string notes = null);

        List<RequestHistory> GetByRequestId(int requestId);
    }
}
