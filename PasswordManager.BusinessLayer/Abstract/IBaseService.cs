using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetAll();
        Task Add(T entity, int? id = null);
        Task<T> GetById(int id);
        Task Remove(int id);
        Task Update(T entity);
    }
}

