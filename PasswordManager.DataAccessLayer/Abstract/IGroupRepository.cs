using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.DataAccessLayer.Abstract
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task AddUserToGroup(int userID, int groupID);
        Task AddGroupToRole(int groupID, int roleID);
        Task<Group> UserGroupRoleCheck(int userID, int roleID);
        Task<List<Group>> GetAllByCompanyId(int companyId);
    }
}
