using Microsoft.Extensions.Configuration;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class UserLevelService : BaseService<UserLevel>, IUserLevelService
    {
        private IUserLevelRepository _userLevelRepository;
        private IConfiguration _configuration;

        public UserLevelService(IUserLevelRepository userLevelRepository, IConfiguration configuration)
        {
            _userLevelRepository = userLevelRepository;
            _configuration = configuration;
        }

        public async Task Add(UserLevel entity, int? id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var levelID = await _userLevelRepository.CompanyLevel_Add(entity, conn);

                        entity.LevelID = levelID;

                        await _userLevelRepository.LangAdd(entity, conn);
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

        public async Task<List<UserLevel>> GetAll()
        {
            return await _userLevelRepository.List();
        }

        public async Task<List<UserLevel>> GetAllByCompanyId(int companyId)
        {
            return await _userLevelRepository.GetAllByCompanyId(companyId);
        }

        public async Task<UserLevel> GetById(int id)
        {
            return await _userLevelRepository.Get(id);  
        }

        public async Task Remove(int id)
        {
            await _userLevelRepository.Remove(id);
        }

        public async Task Update(UserLevel entity)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var levelID = await _userLevelRepository.Update(entity, conn);

                        entity.LevelID = levelID;

                        await _userLevelRepository.LangUpdate(entity, conn);
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
    }
}
