using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        List<User> List();
        void Add(User user);
        void Update(User user);
        void Delete(User user);
        List<User> GetAll(Expression<Func<User, bool>> filter);
        User GetById(int id);
    }
}
