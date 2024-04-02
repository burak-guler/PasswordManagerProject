using Microsoft.Extensions.Configuration;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Repositories;
using System.Data.SqlClient;
using System.Transactions;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class GroupService : BaseService<GroupViewModels>, IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private IConfiguration _configuration;
        

        public GroupService(IGroupRepository groupRepository,IConfiguration configuration)
        {
            _groupRepository = groupRepository;
            _configuration = configuration;
            
        }
        public async Task Add(GroupViewModels entity, int? id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var groupID = await _groupRepository.Add(entity, conn);

                        entity.GroupID = groupID;

                        await _groupRepository.LangAdd(entity, conn);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task AddGroupToRole(int groupID, int roleID, int currentID)
        {
            

            await _groupRepository.AddGroupToRole(groupID, roleID);  
        }

        public async Task AddUserToGroup(int userID, int groupID, int currentID)
        {          

            await _groupRepository.AddUserToGroup(userID,groupID);  
        }

        public async Task<List<GroupViewModels>> GetAll()
        {
            return await _groupRepository.List();   
        }

        public async Task<List<GroupViewModels>> GetAllByCompanyId(int companyId)
        {
            
            return await _groupRepository.GetAllByCompanyId(companyId);
        }

        public async Task<List<GroupRoleViewModel>> GetAllGroupRoleByGrouID(int groupID)
        {
            return await _groupRepository.GetAllGroupRoleByGrouID(groupID);
        }

        public async Task<GroupViewModels> GetById(int id)
        {          

            return await _groupRepository.Get(id);  
        }

        public async Task Remove(int id)
        {
            

            await _groupRepository.Remove(id);
        }

        public async Task RemoveGroupToRole(int groupRoleID, int currentID)
        {
           

            await _groupRepository.RemoveGroupToRole(groupRoleID);
        }

        public async Task RemoveUserToGroup(int userGroupID, int currentID)
        {
            

            await _groupRepository.RemoveUserToGroup(userGroupID);
        }

        public async Task Update(GroupViewModels entity)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var groupID = await _groupRepository.Update(entity, conn);

                        entity.GroupID = groupID;

                        await _groupRepository.LangUpdate(entity, conn);
                        scope.Complete();
                    }
                    catch (Exception ex)
                    {
                        scope.Dispose();
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        public async Task<List<UserViewModels>> UserGroup_BYGroupID(int groupID)
        {
            return await _groupRepository.UserGroup_BYGroupID(groupID);
        }

        public async Task<List<GroupViewModels>> UserGroup_BYUserID(int userID)
        {
            return await _groupRepository.UserGroup_BYUserID(userID);    
        }
    }
}
