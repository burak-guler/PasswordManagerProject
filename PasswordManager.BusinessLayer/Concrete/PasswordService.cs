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

        public PasswordService(IPasswordRepository passwordRepository, IUserLevelRepository userLevelRepository)
        {
            _passwordRepository = passwordRepository;
            _userLevelRepository = userLevelRepository;
        }

        public async Task Add(Password entity, int? id)
        {
            var level = await _userLevelRepository.Get(id.Value);
            if (level != null)
            {
                if (level.LevelName=="Admin")
                {
                    entity.PasswordValue = Encrypt.OnPostEncrypt(entity.PasswordValue);
                    await _passwordRepository.Add(entity);
                }
                else { throw new UnauthorizedAccessException("Kullanıcı yetki dışı."); }
            }
            else { throw new UnauthorizedAccessException("Kullanıcı bulunamadı"); }
            
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
