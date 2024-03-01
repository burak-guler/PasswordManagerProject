using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IRepository<T>
    {
        Task <List<T>> List();
        Task Add(T value);
        Task<T> Get(int id);
        Task Remove(int id);
        Task Update(T value);
    }
}
