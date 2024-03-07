using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;


namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IUserService
    {
        Task< List<User>> GetUserList();
        Task UserAdd(User user);
        Task<User> GetUser(int id);
        Task UserRemove(int id);
        Task UserUpdate(User user);
        Task<User> Login(User user);     
    }
}
