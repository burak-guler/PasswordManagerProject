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
using static Dapper.SqlMapper;

namespace PasswordManager.BusinessLayer.Concrete
{
    public class PasswordService : BaseService<PasswordViewModels>, IPasswordService
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

        public async Task Add(PasswordViewModels entity, int? id)
        {//admin olup olmaması ve kişinin veya girdiği grubun şifre ekleme rolü varmı yokmu şartlarına bakılması.
            var user = await _userRepository.Get(id.Value);
            var userLevel = await _userLevelRepository.Get(user.LevelID);
            if (userLevel.LevelName=="Admin") 
            {
                entity.PasswordValue = Encrypt.OnPostEncrypt(entity.PasswordValue);
                await _passwordRepository.Add(entity);
            }
            else
            {
                int roleID = (int)Role.UserRole.CreatePassword;

                var userRoleCheck = await _userRepository.RoleCheck(roleID, user.UserID);

                var groupRoleCheck = await _groupRepository.UserGroupRoleCheck(user.UserID, roleID);

                if (userRoleCheck != null || groupRoleCheck != null)
                {
                    if (entity.LevelID >= user.LevelID)
                    {
                        entity.PasswordValue = Encrypt.OnPostEncrypt(entity.PasswordValue);
                        await _passwordRepository.Add(entity);
                    }
                    else
                    {
                        throw new Exception("Kullanıcı level seviyesi yetersiz");
                    }
                }
                else { throw new Exception("Kullanıcı rolü eksik"); }
            }                      
        }

        public async Task<List<PasswordViewModels>> GetAll()
        {
            return await _passwordRepository.List();
        }

        public async Task<PasswordViewModels> GetById(int id, int currentID)
        {
            var user = await _userRepository.Get(currentID);
            var userLevel = await _userLevelRepository.Get(user.LevelID);
            if (userLevel.LevelName == "Admin")
            {
                var value = await _passwordRepository.Get(id);
                value.PasswordValue = Encrypt.Decrypt(value.PasswordValue);
                return value;
            }
            else
            {
                int roleID = (int)Role.UserRole.PasswordAcces;

                var passwordRoleCheck = await _passwordRepository.PASSWORDROLE_CHECK(id,user.UserID,roleID);

                var userRoleCheck = await _userRepository.RoleCheck(roleID, user.UserID);

                var groupRoleCheck = await _groupRepository.UserGroupRoleCheck(user.UserID, roleID);

                if (userRoleCheck != null || groupRoleCheck != null || passwordRoleCheck != null)
                {
                    var value = await _passwordRepository.Get(id);
                    value.PasswordValue = Encrypt.Decrypt(value.PasswordValue);
                    return value;
                }
                else
                {
                    throw new Exception("Kullanıcı rolü eksik");
                }
                
            }

           
        }

        public async Task Remove(int id, int currentID)
        {
            var user = await _userRepository.Get(currentID);
            var userLevel = await _userLevelRepository.Get(user.LevelID);
            if (userLevel.LevelName == "Admin")
            {
                await _passwordRepository.Remove(id);
            }
            else
            {
                int roleID = (int)Role.UserRole.RemovePassword;

                var passwordRoleCheck = await _passwordRepository.PASSWORDROLE_CHECK(id, user.UserID, roleID);

                var userRoleCheck = await _userRepository.RoleCheck(roleID, user.UserID);

                var groupRoleCheck = await _groupRepository.UserGroupRoleCheck(user.UserID, roleID);

                if (userRoleCheck != null || groupRoleCheck != null || passwordRoleCheck!= null)
                {
                    await _passwordRepository.Remove(id); 
                }
                else
                {
                    throw new Exception("Kullanıcı rolü eksik");
                }
            }            
        }

        public async Task Update(PasswordViewModels entity,int currentID)
        {
            var user = await _userRepository.Get(currentID);
            var userLevel = await _userLevelRepository.Get(user.LevelID);
            if (userLevel.LevelName == "Admin")
            {
                entity.PasswordValue = Encrypt.OnPostEncrypt(entity.PasswordValue);
                await _passwordRepository.Update(entity);
            }
            else
            {
                int roleID = (int)Role.UserRole.UpdatePassword;

                var passwordRoleCheck = _passwordRepository.PASSWORDROLE_CHECK(entity.PasswordID, user.UserID, roleID);

                var userRoleCheck = _userRepository.RoleCheck(roleID, user.UserID);

                var groupRoleCheck = _groupRepository.UserGroupRoleCheck(user.UserID, roleID);

                if (entity.LevelID >= user.LevelID)
                {
                    if (userRoleCheck != null || groupRoleCheck != null || passwordRoleCheck != null)
                    {
                        entity.PasswordValue = Encrypt.OnPostEncrypt(entity.PasswordValue);
                        await _passwordRepository.Update(entity);
                    }
                    else
                    {
                        throw new Exception("Kullanıcı rolü eksik");
                    }
                }
                else { throw new Exception("Kullanıcı level seviyesi yetersiz"); }
            }
            
        }

        public async Task AddUserToPasswordAcces(int passwordID, int userID, int roleID)
        {
            await _passwordRepository.AddUserToPasswordAcces(passwordID, userID, roleID);
        }

        public async Task<List<PasswordViewModels>> GetAllByCompanyId(int companyId)
        {
            return await _passwordRepository.GetAllByCompanyId(companyId);
        }

        public async Task RemoveUserToPasswordAcces(int passwordID, int userID, int roleID)
        {
            await _passwordRepository.RemoveUserToPasswordAcces(passwordID, userID, roleID);
        }

        public async Task<List<PasswordViewModels>> GetAllByUserId(int userID)
        {
            var passwordList = await _passwordRepository.GetAllByUserId(userID);
            List<PasswordViewModels> decryptedPasswords = new List<PasswordViewModels>();

            foreach (var value in passwordList)
            {
                string decryptedPasswordValue = Encrypt.Decrypt(value.PasswordValue);
                decryptedPasswords.Add(new PasswordViewModels
                {
                    PasswordID = value.PasswordID,
                    UserID = value.UserID,
                    UserName = value.UserName,
                    CategoryName = value.CategoryName,
                    LevelName = value.LevelName,    
                    CategoryID = value.CategoryID,
                    CompanyName = value.CompanyName,    
                    PasswordValue = decryptedPasswordValue,
                    LevelID = value.LevelID,
                    CompanyID = value.CompanyID,
                    IsActive = value.IsActive
                });
            }

            return decryptedPasswords;
        }

        public async Task<List<PasswordViewModels>> PasswordAccesGetList(int userID, int roleID)
        {
           var passwordList = await _passwordRepository.PasswordAccesGetList(userID, roleID);
            List<PasswordViewModels> decryptedPasswords = new List<PasswordViewModels>();

            foreach (var value in passwordList)
            {
                string decryptedPasswordValue = Encrypt.Decrypt(value.PasswordValue);
                decryptedPasswords.Add(new PasswordViewModels
                {
                    PasswordID = value.PasswordID,
                    UserID = value.UserID,
                    UserName = value.UserName,
                    CategoryName = value.CategoryName,
                    LevelName = value.LevelName,
                    CategoryID = value.CategoryID,
                    CompanyName = value.CompanyName,
                    PasswordValue = decryptedPasswordValue,
                    LevelID = value.LevelID,
                    CompanyID = value.CompanyID,
                    IsActive = value.IsActive,
                    RoleID = value.RoleID
                });
            }

            return decryptedPasswords;
        }
    }
}
