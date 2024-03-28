using Microsoft.Extensions.Configuration;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Query;
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
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserLevelRepository _userLevelRepository;
        private readonly IConfiguration _configuration;

        public CompanyService(ICompanyRepository companyRepository, IUserLevelRepository userLevelRepository, IConfiguration configuration)
        {
            _companyRepository = companyRepository;
            _userLevelRepository = userLevelRepository;
            _configuration = configuration;
        }
        public async Task Add(Company entity, int? id)
        {
            using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await conn.OpenAsync();
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        var companyId = await _companyRepository.Add(entity, conn);

                        UserLevel level = new UserLevel()
                        {
                            CreationDate = DateTime.Now,
                            IsActive = true,
                            LangID = 1,
                            LevelName = "Admin",
                            CompanyID = companyId,
                        };

                        int levelID= await _userLevelRepository.CompanyLevel_Add(level,conn);

                        level.LevelID = levelID;

                        await _userLevelRepository.LangAdd(level,conn); 
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

        public async Task<List<Company>> GetAll()
        {
           return await  _companyRepository.List();
        }

        public async Task<Company> GetById(int id)
        {
           return await _companyRepository.Get(id);
        }

        public async Task Remove(int id)
        {
          await  _companyRepository.Remove(id);
        }

        public async Task Update(Company entity)
        {
            await _companyRepository.Update(entity);
        }
    }
}
