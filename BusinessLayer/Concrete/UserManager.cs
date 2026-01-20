using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Repositories;
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
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager()
        {
            _userDal = new EFUserDal();
        }

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void Add(User user)
        {
            var exists = _userDal.Get(x => x.Email == user.Email);
            if (exists != null)
            {
                throw new Exception("Bu e-posta zaten kayıtlı!");
            }
            _userDal.Insert(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public List<User> List()
        {
            return _userDal.List();
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter)
        {
            return _userDal.List(filter);
        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }

        public User GetById(int id)
        {
            return _userDal.Get(x => x.UserId == id);
        }
    }
}
