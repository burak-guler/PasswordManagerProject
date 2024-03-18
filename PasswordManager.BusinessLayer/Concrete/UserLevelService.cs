using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.Core.Entity;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class UserLevelService : IUserLevelService
    {
        private IUserLevelRepository _userLevelRepository;

        public UserLevelService(IUserLevelRepository userLevelRepository)
        {
            _userLevelRepository = userLevelRepository;
        }

        public async Task Add(UserLevel entity)
        {
           await _userLevelRepository.Add(entity);   
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
            await _userLevelRepository.Update(entity);
        }
    }
}
