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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Add(User entity)
        {
            await _userRepository.Add(entity);
        }

        public async Task AddUserToRole(int userID, int roleID)
        {
            await _userRepository.AddUserToRole(userID, roleID);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.List();
        }

        public async Task<List<User>> GetAllByCompanyId(int companyId)
        {
            return await _userRepository.GetAllByCompanyId(companyId);
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.Get(id);
        }

        public async Task<User> Login(User user)
        {
           return await _userRepository.Login(user);
        }

        public async Task Remove(int id)
        {
            await _userRepository.Remove(id);
        }

        public async Task Update(User entity)
        {
            await _userRepository.Update(entity);
        }
        
    }
}
