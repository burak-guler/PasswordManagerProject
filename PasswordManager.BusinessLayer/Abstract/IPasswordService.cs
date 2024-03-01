using PasswordManager.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Abstract
{
    public interface IPasswordService
    {
        Task< List<Password>> GetPasswordList();
        Task PasswordAdd(Password password);
        Task <Password> GetPassword(int id);
        Task PasswordRemove(int id);
        Task PasswordUpdate(Password password);
    }
}
