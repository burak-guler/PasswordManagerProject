using PasswordManager.BusinessLayer.Abstract;
using PasswordManager.BusinessLayer.Encryption;
using PasswordManager.Core.Entity;
using PasswordManager.Core.Models;
using PasswordManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IUserLevelRepository _userLevelRepository;
        private readonly IUserRepository _userRepository;   
        private readonly IGroupRepository _groupRepository;

        public PasswordService(IPasswordRepository passwordRepository, IUserLevelRepository userLevelRepository, IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _passwordRepository = passwordRepository;
            _userLevelRepository = userLevelRepository;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }

        public async Task Add(Password entity, int? id)
        {
            var user = await _userRepository.Get(id.Value);

            int roleID = (int)Role.UserRole.CreatePassword;

            var userRoleCheck = _userRepository.RoleCheck(roleID, user.UserID);

            var groupRoleCheck = _groupRepository.UserGroupRoleCheck(user.UserID,roleID);

            if (user.LevelID >= entity.LevelID) 
            {
                if (userRoleCheck != null || groupRoleCheck != null)
                {
                    entity.PasswordValue = Encrypt.OnPostEncrypt(entity.PasswordValue);
                    await _passwordRepository.Add(entity);
                }
                else
                {
                    throw new Exception("Kullanıcı rolü eksik");
                }
            }
            else { throw new Exception("Kullanıcı level seviyesi yetersiz");}
            
        }

        public async Task<List<Password>> GetAll()
        {
            return await _passwordRepository.List();
        }

        public async Task<Password> GetById(int id)
        {
            var value = await _passwordRepository.Get(id);
            value.PasswordValue = Encrypt.Decrypt(value.PasswordValue);
            return value;
        }

        public async Task Remove(int id)
        {
            await _passwordRepository.Remove(id);
        }

        public async Task Update(Password entity)
        {
            await _passwordRepository.Update(entity);
        }

        public async Task AddUserToPasswordAcces(int passwordID, int userID, int roleID)
        {
            await _passwordRepository.AddUserToPasswordAcces(passwordID,userID,roleID);
        }

        public async Task<List<Password>> GetAllByCompanyId(int companyId)
        {
            return await _passwordRepository.GetAllByCompanyId(companyId);
        }
    }
}
