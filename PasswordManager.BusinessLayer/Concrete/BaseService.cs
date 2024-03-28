using PasswordManager.BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        public Task Add(T entity, int? id = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }

}
