using Microsoft.AspNetCore.Http;
using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Encryption;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using PasswordManager.DataAccessLayer.Concrete.Repositories;
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
        private readonly IUserLevelRepository _userLevelRepository;

        public UserService(IUserRepository userRepository, IUserLevelRepository userLevelRepository)
        {
            _userRepository = userRepository;
            _userLevelRepository = userLevelRepository;
        }

        public async Task Add(User entity, int? id)
        {
            var level = await _userLevelRepository.Get(id.Value);
            if (level != null)
            {
                if (level.LevelName == "Admin")
                {
                    var userCheck = await _userRepository.UserCheck(entity);
                    if (userCheck == null)
                    {
                        await _userRepository.Add(entity);
                    }
                    else { throw new InvalidOperationException("Kullanıcı Adını değiştirin."); }
                }
                else { throw new UnauthorizedAccessException("Kullanıcı yetki dışı."); }
            }
            else { throw new UnauthorizedAccessException("Kullanıcı bulunamadı"); }                     

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
