using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
       Task<User> Login(User user);       
    }
}
