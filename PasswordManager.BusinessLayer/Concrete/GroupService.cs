using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task Add(Group entity, int? id)
        {
            await _groupRepository.Add(entity);
        }

        public async Task AddGroupToRole(int groupID, int roleID)
        {
            await _groupRepository.AddGroupToRole(groupID, roleID);  
        }

        public async Task AddUserToGroup(int userID, int groupID)
        {
            await _groupRepository.AddUserToGroup(userID,groupID);  
        }

        public async Task<List<Group>> GetAll()
        {
            return await _groupRepository.List();   
        }

        public async Task<List<Group>> GetAllByCompanyId(int companyId)
        {
            return await _groupRepository.GetAllByCompanyId(companyId);
        }

        public async Task<Group> GetById(int id)
        {
            return await _groupRepository.Get(id);  
        }

        public async Task Remove(int id)
        {
            await _groupRepository.Remove(id);
        }

        public async Task Update(Group entity)
        {
            await _groupRepository.Update(entity);
        }
    }
}
