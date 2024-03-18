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
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;

        public PasswordService(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task Add(Password entity)
        {
            //rsa
            //aes
            //public & private key

            await _passwordRepository.Add(entity);
        }

        public async Task<List<Password>> GetAll()
        {
            return await _passwordRepository.List();
        }

        public async Task<Password> GetById(int id)
        {
            return await _passwordRepository.Get(id);
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
